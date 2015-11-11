using UnityEngine;
using System.Collections;

public class Sanji : Character {
	public int neutralHeavyDmg = 10;

	public void neutralHeavy(GameObject collider, GameObject hitter) {
		// I think we should also apply the movement vector here for the collider, current implementation is actually quite bad
		// as the collider sets its own direction, which has nothing to do with what hit it. Didn't want to go too far on changing
		// everything for now though.
		PlayerMovement player = collider.GetComponent<PlayerMovement> ();
		float direction = hitter.transform.position.x - collider.transform.position.x;

		player.knockbackVector = new Vector2 (direction > 0 ? -1.5f : 1.5f, 1.5f);
		player.wasThrown (direction);

		collider.GetComponent<Health> ().Damage (neutralHeavyDmg);
	}

}
