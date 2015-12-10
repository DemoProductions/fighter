
using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private Vector2 pos;
	private Vector2 size = new Vector2(120, 20);
	private float health; // full hp if health is 1 on the gui
	private int maxHealth;
	private Texture2D emptyTex;
	private Texture2D fullTex;
	private GUIStyle healthBarStyle;

	private string REFERENCE_PLAYER;

	// Color ranges for the healthbar which change depending on the player's health
	private int UPPER_GREEN_RANGE = 100;
	private int LOWER_GREEN_RANGE = 70;
	private int UPPER_YELLOW_RANGE = 70;
	private int LOWER_YELLOW_RANGE = 30;
	private int UPPER_RED_RANGE = 30;
	private int LOWER_RED_RANGE = 0;

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

	void Update() {
		GameObject player = GameObject.Find (REFERENCE_PLAYER);
		if (player) {
			health = (float) player.GetComponent<Health> ().hp / (float) maxHealth;
		} else { // player has no health points
			health = 0;
		}
	}

	void OnGUI() {
		healthBarStyle = new GUIStyle (GUI.skin.box);
		
		int currentHealth = GameObject.Find (REFERENCE_PLAYER).GetComponent<Health> ().hp;
		
		// note: I have the feeling that using textures like this may cause memory leaks. Will need to do further reading.
		// Each colored texture is currently created with size 1 height and height
		if (currentHealth > LOWER_GREEN_RANGE && currentHealth <= UPPER_GREEN_RANGE) {
			healthBarStyle.normal.background = makeTex (1, 1, new Color (0f, 1f, 0f, 1f));
		} else if (currentHealth > LOWER_YELLOW_RANGE && currentHealth <= UPPER_YELLOW_RANGE) {
			healthBarStyle.normal.background = makeTex (1, 1, new Color (1f, 0.92f, 0.016f, 1f));
		} else if (currentHealth > LOWER_RED_RANGE && currentHealth <= UPPER_RED_RANGE) {
			healthBarStyle.normal.background = makeTex (1, 1, new Color (1f, 0f, 0f, 1f));
		}
		
		GUI.BeginGroup (new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
		GUI.BeginGroup (new Rect (0, 0, size.x * health, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), fullTex, healthBarStyle);
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	
	// Creates a texture with the specified width, height, and color
	private Texture2D makeTex(int width, int height, Color col) {
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i ) {
			pix[i] = col;
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}
}
