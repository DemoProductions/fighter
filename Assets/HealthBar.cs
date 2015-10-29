using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public Vector2 pos = new Vector2(20, 40);
	public Vector2 size = new Vector2(120, 20);
	private float health; // full hp if health is 1 on the gui
	private float maxHealth;
	private Texture2D emptyTex;
	private Texture2D fullTex;

	void Start() {
		// Only a healthbar for player1 for now, need to think of how to handle a healthbar
		// for both players cleanly
		GameObject player = GameObject.Find ("player1");
		maxHealth = player.GetComponent<Health> ().hp;
		if (player) {
			maxHealth = player.GetComponent<Health>().hp;
		}
	}

	void OnGUI() {
		GUI.BeginGroup (new Rect(pos.x, pos.y, size.x, size.y));
		    GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
		    GUI.BeginGroup (new Rect (0, 0, size.x * health, size.y));
		        GUI.Box (new Rect (0, 0, size.x, size.y), fullTex);
		    GUI.EndGroup ();
		GUI.EndGroup ();
	}

	void Update() {
		GameObject player = GameObject.Find ("player1");
		if (player) {
			health = player.GetComponent<Health> ().hp / maxHealth;
		} else { // player has no health points
			health = 0;
		}
	}
}
