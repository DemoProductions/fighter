using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public Vector2 pos = new Vector2 (20, 40);
	public Vector2 size = new Vector2(60, 20);
	private float health; // full hp if health is 1 on the gui
	private Texture2D emptyTex;
	private Texture2D fullTex;

	void Start() {
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
		GameObject player = GameObject.Find ("player");
		if (player) {
			health = player.GetComponent<Health> ().hp / 100;
		} else { // player has no health points
			health = 0;
		}
	}
}
