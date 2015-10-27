using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	int health;
	public Vector2 pos = new Vector2 (20, 40);
	public Vector2 size = new Vector2(60, 20);
	public Texture2D emptyTex;
	public Texture2D fullTex;

	void Start() {
		// full hp if health is 1 on the gui
		health = GameObject.Find ("player").GetComponent<Health> ().hp / 100;
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
		// can use an effect like this if we want the healthbar to
		// become full gradually on spawn instead of immediately starting
		// at full health. health would start at 0
		/*if (health < 1) {
			health = Time.time * 0.4f;
		}*/

	}
}
