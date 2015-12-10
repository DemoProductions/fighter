using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

	public static int player1character;
	public static int player2character;
	
	public Image player1image;
	public Image player2image;

	public Sprite[] characters;

	public float holdDelta;
	public float player1delta;
	public float player2delta;

	// Use this for initialization
	void Start () {
		player1character = 0;
		player2character = 1;
		player1image = GameObject.Find("Char1").GetComponent<Image>();
		player2image = GameObject.Find("Char2").GetComponent<Image>();
		player1delta = 0;
		player2delta = 0;
		holdDelta = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		//update character holds
		player1delta -= Time.deltaTime;
		player2delta -= Time.deltaTime;

		//check for start
		if (Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel("testscene2");
		}

		//update character select int
		if (player1delta < 0) {
			if (Input.GetAxis ("Horizontal1") > 0 || Input.GetAxis ("Vertical1") < 0) {
				player1character++;
				player1delta = holdDelta;
			} else if (Input.GetAxis ("Horizontal1") < 0 || Input.GetAxis ("Vertical1") > 0) {
				player1character--;
				player1delta = holdDelta;
			}
		}
		if (player2delta < 0) {
			if (Input.GetAxis ("Horizontal2") > 0 || Input.GetAxis ("Vertical2") < 0) {
				player2character++;
				player2delta = holdDelta;
			} else if (Input.GetAxis ("Horizontal2") < 0 || Input.GetAxis ("Vertical2") > 0) {
				player2character--;
				player2delta = holdDelta;
			}
		}

		//correct values
		if (player1character < 0) {
			player1character = characters.Length - 1;
		}
		else if (player1character >= characters.Length) {
			player1character = 0;
		}
		if (player2character < 0) {
			player2character = characters.Length - 1;
		}
		else if (player2character >= characters.Length) {
			player2character = 0;
		}

		//update image
		player1image.sprite = characters [player1character];
		player2image.sprite = characters [player2character];
	}
}
