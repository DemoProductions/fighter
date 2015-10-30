using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int hp = 100;

	public void Damage(int damage) {
		hp -= damage;

		if (hp <= 0) {
			Destroy (gameObject);
		}
	}
}
