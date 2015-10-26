using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int speed;
	public float time;
	public bool finished;
	public int jumps;
	public float yvelocity;
	private float lastyvelocity;
	private float jumpdelta;
	Animator anim;
	private bool right;
	
	// Use this for initialization
	void Start () {
		speed = 1200;
		time = 0;
		finished = false;
		jumps = 0;
		yvelocity = 0;
		jumpdelta = 0;
		anim = GetComponent<Animator>();
		right = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float x = 0.0f;
		// don't move while kicking
		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("kick_right")) {
			x = Input.GetAxis ("Horizontal");
		}

		//kick logic
		if (Input.GetKeyDown (KeyCode.E)) {
			anim.SetTrigger("kick");
			anim.SetBool ("running", false);
		}

		//left right animation logic
		//remember last direction (when x = 0 it will leave in the previous state)
		if (x > 0) {
			right = true;

			// Multiply the player's x local scale by -1
			if ( transform.localScale.x < 0) {
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				transform.localScale = scale;
			}
		} else if (x < 0) {
			right = false;

			// Multiply the player's x local scale by -1
			if ( transform.localScale.x > 0) {
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				transform.localScale = scale;
			}
		}
		anim.SetBool ("running", x != 0);

		//start character falling. Default falling felt bad. Not necessary.
		if (jumps > 0) {
			yvelocity -= .05f;
		}

		//double jump delta. Should replace with bool to know if jump was released.
		jumpdelta += Time.deltaTime;
		if (!finished) {
			time += Time.deltaTime;
		}

		//jump logic
		if (jumpdelta > .2 && Input.GetAxis ("Vertical") > 0 && jumps < 2) {
			jumps++;
			yvelocity = 1.5f;
			jumpdelta = 0;
		} else if (jumps > 0 && Input.GetAxis ("Vertical") < 0) {
			yvelocity -= .10f;
		}

		//apply movement
		GetComponent<Rigidbody2D>().velocity = new Vector2(x, yvelocity) * Time.deltaTime * speed;
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
}
