using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject sanji;
	public GameObject robocop;
	public GameObject[] players = new GameObject[2];

	// Use this for initialization
	void Start () {
		// we need to figure out a way to determine player count before this step, assuming 2 for now
		// also need to determine spawn positions x,y. Could just input to this script based on scene?
		PlayerMovement player1 = (Instantiate (sanji, new Vector2 (0, -1), Quaternion.identity) as GameObject).GetComponent<PlayerMovement>();
		PlayerMovement player2 = (Instantiate (robocop, new Vector2 (10, -1), Quaternion.identity) as GameObject).GetComponent<PlayerMovement>();
		// set player specific settings
		player1.setPlayer (1);
		player2.setPlayer (2);
		player1.setCharacter (new Sanji ());
		player2.setCharacter (new Sanji ());
		FaceCenter (player1);
		FaceCenter (player2);
		// add to camera
		CameraMovement camera = GameObject.Find ("Main Camera").GetComponent<CameraMovement>();
		players[0] = player1.gameObject;
		players[1] = player2.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Face center of world (x = 0)
	void FaceCenter(PlayerMovement player) {
		if (player.transform.position.x <= 0) {
			player.right = true;
		} else {
			player.right = false;
		}
	}
}
