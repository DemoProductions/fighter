using UnityEngine;
using System.Collections;

public class HitBoxManager : ColliderManager {
	
	public override void OnTriggerEnter2D(Collider2D collider)
	{
		string reaction; //debug var
		// ignore parent collisions
		if (collider.gameObject.name == this.transform.parent.name) {
			Debug.Log ("parent collision ignored");
			return;
		}

		// debug, loop, should simplify to simply the if statement and its function call
		if (collider.gameObject.name.Contains ("HurtBox") && collider.transform.parent.name != this.transform.parent.name) {
			reaction = "hit";
			GetComponentInParent<PlayerMovement> ().hit (collider);
		} else {
			reaction = "ignored";
		}
		Debug.Log (this.gameObject.name + " on " + this.transform.parent.name + " hit " + collider.gameObject.name + ": " + reaction);
	}

}