using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int speed;
	public float time;
	public bool finished;
	public int jumps;
	public float xvelocity;
	public float yvelocity;
	private float lastyvelocity;
	private float jumpdelta;
	Animator anim;
	public bool right;
	
	// Use this for initialization
	void Start () {
		speed = 1200;
		time = 0;
		finished = false;
		jumps = 0;
		xvelocity = 0;
		yvelocity = 0;
		jumpdelta = 0;
		anim = GetComponent<Animator>();
		right = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//double jump delta. Should replace with bool to know if jump was released.
		jumpdelta += Time.deltaTime;
		if (!finished) {
			time += Time.deltaTime;
		}

//		xvelocity = 0.0f;
		// don't move while kicking
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("thrown")) {
			if(right)
				xvelocity = -1f;
			else
				xvelocity = 1f;
		}
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("kick")) {
			xvelocity = 0f;
		} 
		else {
			xvelocity = Input.GetAxis ("Horizontal");
			//remember last direction (when x = 0 it will leave in the previous state)
			if (xvelocity > 0) {
				right = true;
			}
			else if (xvelocity < 0) {
				right = false;
			}

			if (Input.GetKeyDown (KeyCode.E)) {
				anim.SetTrigger("kick");
				anim.SetBool ("running", false);
			}

			anim.SetBool ("running", xvelocity != 0);

			//jump logic
			if (jumpdelta > .2 && Input.GetAxis ("Vertical") > 0 && jumps < 2) {
				jumps++;
				yvelocity = 1.5f;
				jumpdelta = 0;
			} else if (jumps > 0 && Input.GetAxis ("Vertical") < 0) {
				yvelocity -= .10f;
			}
		}

		//left right animation logic
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

		//start character falling. Default falling felt bad. Not necessary.
		if (jumps > 0) {
			yvelocity -= .05f;
		}

		//apply movement
		GetComponent<Rigidbody2D>().velocity = new Vector2(xvelocity, yvelocity) * Time.deltaTime * speed;
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.name == "floor") {
			jumps = 0;
			yvelocity = 0;
		}
	}
	
	void OnCollisionStay2D(Collision2D collision) {
		
	}
	
	void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.gameObject.name == "floor") {
			jumps = 1;
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {

	}

	public void hit(Collider2D collider) {
		Debug.Log ("hit");
		if (collider.gameObject.name == "player2") {
			collider.gameObject.GetComponent<PlayerMovement>().wasHit (right);
//			collider.gameObject.GetComponent<HitBoxManager>().clearHitBox();
		}
	}

	public void wasHit(bool right) {
		Debug.Log ("was hit");
		this.right = !right;
		if (right) {
			xvelocity = -1f;
		} else {
			xvelocity = 1f;
		}
		anim.Play ("thrown");
	}
}
