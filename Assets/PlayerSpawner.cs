﻿using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject sanji;

	// Use this for initialization
	void Start () {
		// we need to figure out a way to determine player count before this step, assuming 2 for now
		// also need to determine spawn positions x,y. Could just input to this script based on scene?
		GameObject player1 = Instantiate (sanji, new Vector2 (3, -3), Quaternion.identity) as GameObject;
		GameObject player2 = Instantiate (sanji, new Vector2 (-3, -3), Quaternion.identity) as GameObject;
		player1.GetComponent<PlayerMovement>().setPlayer (1);
		player2.GetComponent<PlayerMovement>().setPlayer (2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}