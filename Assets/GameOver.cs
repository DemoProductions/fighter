using UnityEngine;

public class GameOver : MonoBehaviour
{
	public Texture gameOverTexture;
	public Texture replayTexture;

	void OnGUI()
	{
		const int buttonWidth = 120;
		const int buttonHeight = 60;

		GUI.depth = -1;
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), gameOverTexture, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), replayTexture, ScaleMode.ScaleToFit, true);

		if (
			GUI.Button(
			// Center in X, 1/3 of the height in Y
			new Rect(
			(1 * Screen.width / 3) - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Retry!"
			)
			)
		{
			// Reload the level
			Application.LoadLevel("testscene2");
		}
		
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(
			(2 * Screen.width / 3) - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Quit"
			)
			)
		{
			// Reload the level
			Application.Quit ();
		}
	}
}