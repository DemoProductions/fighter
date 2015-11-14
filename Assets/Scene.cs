using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	public GameObject[] players;
	public GameObject gameOver;
	public int numPlayers;

	// Use this for initialization
	void Start () {
		PlayerSpawner playerSpawner = GameObject.Find ("PlayerSpawner").GetComponent<PlayerSpawner> ();
		players = playerSpawner.players;

		numPlayers = players.Length;

		gameOver = GameObject.Find ("gameover");
	}
	
	// Update is called once per frame
	void Update () {
		// for debugging purposes
		if (Input.GetKeyDown (KeyCode.F1)) {
			Application.LoadLevel ("testscene");
		}

		if (numPlayers <= 1) { // may have to tweak this for draws
			gameOver.GetComponent<GameOver> ().enabled = true;
		}
	}

	public void decrementNumPlayers() {
		numPlayers = numPlayers - 1;
	}
}
