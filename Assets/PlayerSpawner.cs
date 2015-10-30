using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject sanji;
	public GameObject healthBar;

	// Use this for initialization
	void Start () {
		// we need to figure out a way to determine player count before this step, assuming 2 for now
		// also need to determine spawn positions x,y. Could just input to this script based on scene?
		GameObject player1 = Instantiate (sanji, new Vector2 (3, -3), Quaternion.identity) as GameObject;
		GameObject player2 = Instantiate (sanji, new Vector2 (-3, -3), Quaternion.identity) as GameObject;
		// set player specific settings
		player1.GetComponent<PlayerMovement>().setPlayer (1);
		player2.GetComponent<PlayerMovement>().setPlayer (2);
		player1.GetComponent<PlayerMovement> ().setCharacter (new Sanji ());
		player2.GetComponent<PlayerMovement> ().setCharacter (new Sanji ());

		// These positions don't do anything for now. Some groundwork for having 2 healthbars
		GameObject player1HealthBar = Instantiate (healthBar, new Vector2 (20, 40), Quaternion.identity) as GameObject;
		GameObject player2HealthBar = Instantiate (healthBar, new Vector2 (40, 40), Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
