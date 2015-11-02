using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// for debugging purposes
		if (Input.GetKeyDown (KeyCode.F1)) {
			Application.LoadLevel ("testscene");
		}
	}
}
