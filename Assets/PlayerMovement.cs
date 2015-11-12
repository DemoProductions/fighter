﻿using UnityEngine;
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
	public bool thrownRight;
	public Vector2 knockbackVector;
	public HitBoxManager hitboxmanager;
	public HurtBoxManager hurtboxmanager;
	public float raycastJumpLength;

	public Character character;

	public bool jumpReleased;
	
	public bool lightAttackReleased;
	public bool heavyAttackReleased;
	public bool dodgeReleased;

	public string HORIZONTAL;
	public string VERTICAL;
	public string LIGHT_ATTACK;
	public string HEAVY_ATTACK;
	public string DODGE;

	private const string NEUTRAL_HEAVY = "neutral_heavy";

	public const float GRAVITY = .05f;

	private GameObject gameOver;

	// Use this for initialization
	void Start () {
		speed = 1200;
		finished = false;
		jumps = 0;
		xvelocity = 0;
		yvelocity = 0;
		anim = GetComponent<Animator>();
		if (right == null) {
			right = true;
		}
		hitboxmanager = this.gameObject.GetComponentInChildren<HitBoxManager> ();
		hurtboxmanager = this.gameObject.GetComponentInChildren<HurtBoxManager> ();
		raycastJumpLength = 0.1f;
		thrown = false;
		jumpReleased = true;
		heavyAttackReleased = true;
		lightAttackReleased = true;
		dodgeReleased = true;

		gameOver = GameObject.Find ("gameover");
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
			hitboxmanager.clear();
			right = thrownRight;
			// anim.Play immediately skips to thrown animation. Thrown will happen often enough that making a trigger line
			// from every other animation to thrown would be annoying. Thrown default returns to idle, for now.
			thrown = false;
			jumps = 1;
		}

		// Special Animation logic

		// if thrown, ignore user input, velocity is away from the direction you are facing.
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("thrown") 
		    || anim.GetCurrentAnimatorStateInfo (0).IsName ("ko")) {
			xvelocity = knockbackVector.x;
			yvelocity = knockbackVector.y;
			knockbackVector.y -= GRAVITY;
		}
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName (NEUTRAL_HEAVY)) {
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
				anim.SetTrigger(NEUTRAL_HEAVY);
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

		// if you want to see the raycast that determines whether this player can jump
		// or not, turn on Gizmos in the game view
		Debug.DrawRay (new Vector2(GetComponent<BoxCollider2D>().bounds.center.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down * raycastJumpLength, Color.red);
		Debug.DrawRay (new Vector2(GetComponent<BoxCollider2D>().bounds.max.x - .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down * raycastJumpLength, Color.red);
		Debug.DrawRay (new Vector2(GetComponent<BoxCollider2D>().bounds.min.x + .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down * raycastJumpLength, Color.red);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		RaycastHit2D raycastBottomMiddle = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.center.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		RaycastHit2D raycastBottomRight = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.max.x - .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		RaycastHit2D raycastBottomLeft = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.min.x + .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		// if the player can jump on something
		if (raycastBottomMiddle.collider != null || raycastBottomRight.collider != null || raycastBottomLeft.collider != null) {
			jumps = 0;
			yvelocity = 0;
		}
	}
	
	void OnCollisionStay2D(Collision2D collision) {
		RaycastHit2D raycastBottomMiddle = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.center.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		RaycastHit2D raycastBottomRight = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.max.x - .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		RaycastHit2D raycastBottomLeft = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.min.x + .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		// if the player can jump on something
		if (raycastBottomMiddle.collider != null || raycastBottomRight.collider != null || raycastBottomLeft.collider != null) {
			jumps = 0;
			yvelocity = 0;
		}
	}
	
	void OnCollisionExit2D(Collision2D collision) {
		RaycastHit2D raycastBottomMiddle = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.center.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		RaycastHit2D raycastBottomRight = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.max.x - .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		RaycastHit2D raycastBottomLeft = Physics2D.Raycast (new Vector2(GetComponent<BoxCollider2D>().bounds.min.x + .025f, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, raycastJumpLength);
		// if the player can jump on something
		if (raycastBottomMiddle.collider == null && raycastBottomRight.collider == null && raycastBottomLeft.collider == null) {
			jumps = 1;
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.name == "deathbox") {
			Destroy(this.gameObject);
		}
	}

	// pass through setHitBox to child hitbox's hitboxmanager script.
	// this just allows the setHitBox function to be visible and usable by our player level animation, which cannot see the
	// functions in the child element's script.
	public void clearHitbox() {
		hitboxmanager.clear ();
	}
	
	public void setIdleHitBox(ColliderManager.frames val) {
		hitboxmanager.setCollider (ColliderManager.types.idle, val);
	}
	
	public void setRunHitBox(ColliderManager.frames val) {
		hitboxmanager.setCollider (ColliderManager.types.run, val);
	}
	
	public void setNeutralHeavyHitBox(ColliderManager.frames val) {
		hitboxmanager.setCollider (ColliderManager.types.neutral_heavy, val);
	}
	
	public void setNeutralLightHitBox(ColliderManager.frames val) {
		hitboxmanager.setCollider (ColliderManager.types.neutral_light, val);
	}

	public void clearHurtbox() {
		hurtboxmanager.clear ();
	}
	
	public void setIdleHurtBox(ColliderManager.frames val) {
		hurtboxmanager.setCollider (ColliderManager.types.idle, val);
	}
	
	public void setRunHurtBox(ColliderManager.frames val) {
		hurtboxmanager.setCollider (ColliderManager.types.run, val);
	}
	
	public void setNeutralHeavyHurtBox(ColliderManager.frames val) {
		hurtboxmanager.setCollider (ColliderManager.types.neutral_heavy, val);
	}

	public void setNeutralLightHurtBox(ColliderManager.frames val) {
		hurtboxmanager.setCollider (ColliderManager.types.neutral_light, val);
	}

	public void hit(Collider2D collider) {
		// right is direction boolean of the hitting player
		if (anim.GetCurrentAnimatorStateInfo (0).IsName (NEUTRAL_HEAVY)) {
			character.neutralHeavy (collider.transform.parent.gameObject, this.gameObject);
		}
	}

	public void wasThrown(float direction) {
		// set direction to face your attacker and play thrown animation
		if (direction > 0) {
			thrownRight = true;
		}
		else {
			thrownRight = false;
		}
		// anim.Play immediately skips to thrown animation. Thrown will happen often enough that making a trigger line
		// from every other animation to thrown would be annoying. Thrown default returns to idle, for now.
		anim.Play ("thrown");
		thrown = true;
	}

	public void wasKo(float direction) {
		if (direction > 0) {
			thrownRight = true;
		}
		else {
			thrownRight = false;
		}
		anim.Play ("ko");

		//gameOver.GetComponent<GameOver> ().enabled = true;
	}
}