using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	public int numPlayers;
	public GameObject gameOver;

	// Use this for initialization
	void Start () {
		numPlayers = 2; // for now there are always 2 players

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
