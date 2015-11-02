using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int speed;
	public bool finished;
	public int jumps;
	public float xvelocity;
	public float yvelocity;
	private float lastyvelocity;
	Animator anim;
	public bool right;
	public bool thrown;
	public bool thrownright;
	public Vector2 knockbackVector;
	public HitBoxManager hitboxmanager;
	public float raycastJumpLength;

	public Character character;

	public bool jumpReleased;
	
	public bool lightAttackReleased;
	public bool heavyAttackReleased;
	public bool dodgeReleased;

	public string HORIZONTAL;
	public string VERTICAL;
	public string LIGHT_ATTACK; // future kick
	public string HEAVY_ATTACK; // current kick
	public string DODGE;

	public const float GRAVITY = .05f;

	// Use this for initialization
	void Start () {
		speed = 1200;
		finished = false;
		jumps = 0;
		xvelocity = 0;
		yvelocity = 0;
		anim = GetComponent<Animator>();
		right = true;
		hitboxmanager = this.gameObject.GetComponentInChildren<HitBoxManager> (); 
		raycastJumpLength = 0.1f;
		thrown = false;
		jumpReleased = true;
		heavyAttackReleased = true;
		lightAttackReleased = true;
		dodgeReleased = true;
	}

	// set player relevant information
	public void setPlayer(int player) {
		switch (player) {
		case 1:
			gameObject.name = "player1";
			HORIZONTAL = "Horizontal1";
			VERTICAL = "Vertical1";
			LIGHT_ATTACK = "LightAttack1";
			HEAVY_ATTACK = "HeavyAttack1";
			DODGE = "Dodge1";
			break;
		case 2:
			gameObject.name = "player2";
			HORIZONTAL = "Horizontal2";
			VERTICAL = "Vertical2";
			LIGHT_ATTACK = "LightAttack2";
			HEAVY_ATTACK = "HeavyAttack2";
			DODGE = "Dodge2";
			break;
		}
	}

	// set character
	public void setCharacter(Character character) {
		this.character = character;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Hit logic. Delayed from actual hit to avoid adjusting the frame of the hit itself.
		// After being hit, we will set hit or thrown to true (depending on hi strength), and play it now.
		if (thrown) {
			hitboxmanager.clearHitBox();
			right = thrownright;
			// anim.Play immediately skips to thrown animation. Thrown will happen often enough that making a trigger line
			// from every other animation to thrown would be annoying. Thrown default returns to idle, for now.
			anim.Play ("thrown");
			thrown = false;
			jumps = 1;
		}

		// Special Animation logic

		// if thrown, ignore user input, velocity is away from the direction you are facing.
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("thrown")) {
			xvelocity = knockbackVector.x;
			yvelocity = knockbackVector.y;
			knockbackVector.y -= GRAVITY;
		}
		// if kicking, ignore user input, don't move
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("kick")) {
			xvelocity = 0f;
		}
		// else user input as normal (run and jump)
		// Anything that must NOT happen in special animation, but should happen normally goes here
		else {
			xvelocity = Input.GetAxis (HORIZONTAL);

			// save direction boolean
			// remembers last direction (when x = 0 it will leave in the previous state)
			if (xvelocity > 0) {
				right = true;
			}
			else if (xvelocity < 0) {
				right = false;
			}

			if (Input.GetAxis(VERTICAL) == 0) {
				jumpReleased = true;
			}

			// Check for input releases
			if (Input.GetAxis (HEAVY_ATTACK) == 0){
				heavyAttackReleased = true;
			}
			if (Input.GetAxis (LIGHT_ATTACK) == 0){
				lightAttackReleased = true;
			}
			if (Input.GetAxis (DODGE) == 0){
				dodgeReleased = true;
			}

			// User Input priority chain
			// if trying heavy attack
			if (heavyAttackReleased && Input.GetAxis (HEAVY_ATTACK) > 0) {
				heavyAttackReleased = false;
				anim.SetTrigger("kick");
			}
			// else run, if necessary
			else {
				anim.SetBool ("running", xvelocity != 0);
			}

			//jump logic
			if (jumpReleased && Input.GetAxis (VERTICAL) > 0 && jumps < 2) {
				jumps++;
				yvelocity = 1.5f;
				jumpReleased = false;
			} else if (jumps > 0 && Input.GetAxis (VERTICAL) < 0) {
				yvelocity -= .10f;
			}
		}

		// Things that always happen

		// left/right animation logic
		if (right) {
			// Multiply the player's x local scale by -1
			if ( transform.localScale.x < 0) {
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				transform.localScale = scale;
			}
		} else {
			// Multiply the player's x local scale by -1
			if ( transform.localScale.x > 0) {
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				transform.localScale = scale;
			}
		}

		// start character falling. Default falling felt bad. This is not necessary.
		yvelocity -= GRAVITY;

		// apply movement
		GetComponent<Rigidbody2D>().velocity = new Vector2(xvelocity, yvelocity) * Time.deltaTime * speed;

		// raycast
		Debug.DrawRay (transform.position, Vector2.down * raycastJumpLength, Color.red);
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		RaycastHit2D raycastJump = Physics2D.Raycast (transform.position, Vector2.down, raycastJumpLength);
		// if the player can jump on something
		if (raycastJump.collider != null) {
			jumps = 0;
			yvelocity = 0;
		}
	}
	
	void OnCollisionStay2D(Collision2D collision) {
		if (jumps == 0) {
			yvelocity = 0;
		}
	}
	
	void OnCollisionExit2D(Collision2D collision) {
		RaycastHit2D raycastJump = Physics2D.Raycast (transform.position, Vector2.down, raycastJumpLength);
		// if the player can jump on something
		if (raycastJump.collider != null) {
			jumps = 1;
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		
	}

	// pass through setHitBox to child hitbox's hitboxmanager script.
	// this just allows the setHitBox function to be visible and usable by our player level animation, which cannot see the
	// functions in the child element's script.
	public void setHitBox(HitBoxManager.hitBoxes val) {
		hitboxmanager.setHitBox (val);
	}

	public void hit(Collider2D collider) {
		// right is direction boolean of the hitting player
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("kick")) {
			character.kick (collider.gameObject, this.gameObject);
		}
	}
	
	public void wasThrown(float direction) {
		// set direction to face your attacker and play thrown animation
		if (direction > 0) {
			thrownright = true;
		}
		else {
			thrownright = false;
		}
		// anim.Play immediately skips to thrown animation. Thrown will happen often enough that making a trigger line
		// from every other animation to thrown would be annoying. Thrown default returns to idle, for now.
		anim.Play ("thrown");
		thrown = true;
	}
}
