using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float hp = 100;

	public void Damage(float damage) {
		hp -= damage;

		if (hp <= 0) {
			Destroy (gameObject);
		}
	}
}
