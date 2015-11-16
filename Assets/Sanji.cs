using UnityEngine;
using System.Collections;

public class Sanji : Character {
	public int neutralHeavyDmg = 10;
	public int neutralLightDmg = 5;

	public void neutralHeavy(GameObject collider, GameObject hitter) {
		// I think we should also apply the movement vector here for the collider, current implementation is actually quite bad
		// as the collider sets its own direction, which has nothing to do with what hit it. Didn't want to go too far on changing
		// everything for now though.
		PlayerMovement player = collider.GetComponent<PlayerMovement> ();
		float direction = hitter.transform.position.x - collider.transform.position.x;

		Health colliderHealth = collider.GetComponent<Health> ();

		colliderHealth.Damage (neutralHeavyDmg);
		player.knockbackVector = new Vector2 (direction > 0 ? -1.5f : 1.5f, 1.5f);

		// future note: change this to be determined by health, no need to have this if case on every attack
		// instead just on wasThrown and wasHit check and swap to wasKO if health is < 0. Then we only have those two
		// checks for the hp, and won't have to add the check for every attack on every character.
		if (colliderHealth.isKo()) {
			player.wasKo(direction);
		} else {
			player.wasThrown (direction);
		}
	}

	public void neutralLight(GameObject collider, GameObject hitter) {
		PlayerMovement player = collider.GetComponent<PlayerMovement> ();
		float direction = hitter.transform.position.x - collider.transform.position.x;
		
		Health colliderHealth = collider.GetComponent<Health> ();
		
		colliderHealth.Damage (neutralLightDmg);
		player.knockbackVector = new Vector2 (direction > 0 ? -.5f : .5f, 0f);
		
		// future note: change this to be determined by health, no need to have this if case on every attack
		// instead just on wasThrown and wasHit check and swap to wasKO if health is < 0. Then we only have those two
		// checks for the hp, and won't have to add the check for every attack on every character.
		if (colliderHealth.isKo()) {
			player.wasKo(direction);
		} else {
			player.wasHit (direction);
		}
	}
}
