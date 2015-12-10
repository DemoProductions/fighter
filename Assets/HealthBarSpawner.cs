using UnityEngine;
using System.Collections;

public class HealthBarSpawner : MonoBehaviour {

	public GameObject healthBar;
	public Vector2 player1HealthBarPos = new Vector2(20, 40);
	public Vector2 player2HealthBarPos = new Vector2(320, 40);
	public Vector2 healthBarSize = new Vector2(120, 20);

	// Use this for initialization
	void Start () {
		// Position vector stubs will be set depending on the player
		GameObject player1HealthBar = Instantiate (healthBar, new Vector2 (0, 0), Quaternion.identity) as GameObject;
		GameObject player2HealthBar = Instantiate (healthBar, new Vector2 (0, 0), Quaternion.identity) as GameObject;
		player1HealthBar.GetComponent<HealthBar> ().setHealthBar (1, player1HealthBarPos, healthBarSize);
		player2HealthBar.GetComponent<HealthBar> ().setHealthBar (2, player2HealthBarPos, healthBarSize);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
