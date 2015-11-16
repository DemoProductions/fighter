using UnityEngine;

public class Menu : MonoBehaviour{
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
		if(Input.anyKey) Application.LoadLevel("testscene2");
	}
}