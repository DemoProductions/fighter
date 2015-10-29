using UnityEngine;
using System.Collections;

public class Sanji : Character {
	public float kickDmg = 10f; // all kicks deal the same dmg for now

	public void kick(GameObject collider, GameObject hitter) {
		// here we would apply damage unique to Sanji's kick
		// I think we should also apply the movement vector here for the collider, current implementation is actually quite bad
		// as the collider sets its own direction, which has nothing to do with what hit it. Didn't want to go too far on changing
		// everything for now though.
		PlayerMovement player = collider.GetComponent<PlayerMovement> ();
		float direction = hitter.transform.position.x - collider.transform.position.x;

		player.knockbackVector = new Vector2 (direction > 0 ? -1 : 1, 1);
		player.wasThrown (direction);

		collider.GetComponent<Health> ().Damage (kickDmg);
	}

}
