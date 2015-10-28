using UnityEngine;
using System.Collections;

public class Sanji : MonoBehaviour, Character {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void kick(GameObject collider, GameObject hitter) {
		collider.GetComponent<PlayerMovement>().wasThrown (hitter.transform.position.x - collider.transform.position.x);
	}
}
