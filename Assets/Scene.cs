using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	public GameObject[] players;
	public GameObject gameOver;
	public int numPlayers;
	public string currentLevel;

	// Use this for initialization
	void Start () {
		PlayerSpawner playerSpawner = GameObject.Find ("PlayerSpawner").GetComponent<PlayerSpawner> ();
		players = playerSpawner.players;
		gameOver = GameObject.Find ("gameover");
		numPlayers = players.Length;
		currentLevel = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () {
		// for debugging purposes
		/*if (Input.GetKeyDown (KeyCode.F1)) {
			Application.LoadLevel ("testscene");
		}*/

		if (numPlayers <= 1) { // may have to tweak this for draws
			gameOver.GetComponent<GameOver> ().enabled = true;
		}
	}

	public void koPlayer() {
		numPlayers = numPlayers - 1;
	}

	public void deactivatePlayer(string playerName) {
		numPlayers = numPlayers - 1;
		GameObject.Find (playerName).gameObject.SetActive (false);
	}
}
