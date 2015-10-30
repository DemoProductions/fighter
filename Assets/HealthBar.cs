
using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private Vector2 pos;
	private Vector2 size = new Vector2(120, 20);
	private float health; // full hp if health is 1 on the gui
	private int maxHealth;
	private Texture2D emptyTex;
	private Texture2D fullTex;

	private string REFERENCE_PLAYER;

	public void setHealthBar(int player, Vector2 playerHealthBarPos) {
		switch (player) {
		case 1:
			gameObject.name = "player1_healthbar";
			REFERENCE_PLAYER = "player1";
			pos = new Vector2(20, 40);
			break;
		case 2:
			gameObject.name = "player2_healthbar";
			pos = new Vector2(320, 40);
			REFERENCE_PLAYER = "player2";
			break;
		}
	}

	void Start() {
		GameObject player = GameObject.Find (REFERENCE_PLAYER);
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
		GameObject player = GameObject.Find (REFERENCE_PLAYER);
		if (player) {
			health = (float) player.GetComponent<Health> ().hp / (float) maxHealth;
		} else { // player has no health points
			health = 0;
		}
	}
}
