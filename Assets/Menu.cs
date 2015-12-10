using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public GameObject pressAnyKey;

	void Start() {
		StartCoroutine (blinkPressAnyKey ());
	}

//	void OnGUI() {
//		const int buttonWidth = 84;
//		const int buttonHeight = 60;
//
//		Rect buttonRect = new Rect(
//			Screen.width / 2 - (buttonWidth / 2),
//			(2 * Screen.height / 3) - (buttonHeight / 2),
//			buttonWidth,
//			buttonHeight
//			);
//
//		if(GUI.Button(buttonRect,"testscene2")) {
//			Application.LoadLevel("testscene2");
//		}
//	}

	void FixedUpdate() {
		if(Input.anyKey) Application.LoadLevel("character select");
	}

	IEnumerator blinkPressAnyKey() {
		while(true){
			pressAnyKey.SetActive (false);
			yield return new WaitForSeconds(.1f);
			pressAnyKey.SetActive (true);
			yield return new WaitForSeconds(.5f);
		}
	}
}