using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int hp = 100;
	private bool ko = false;

	public void Damage(int damage) {
		hp -= damage;

		if (hp <= 0) {
			ko = true;
		}
	}

	public bool isKo() {
		return ko;
	}
}
