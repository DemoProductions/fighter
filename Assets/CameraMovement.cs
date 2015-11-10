using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject[] players;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float camSpeed;
	public int playercount;

	// Use this for initialization
	void Start () {
		players = GameObject.Find ("PlayerSpawner").GetComponent<PlayerSpawner>().players;
		minX = 0f;
		maxX = 0f;
		minY = 0f;
		maxY = 0f;
		playercount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateBounds();
		CalculateCameraPosAndSize();
	}

	void CalculateBounds() {
		minX = Mathf.Infinity;
		maxX = -Mathf.Infinity;
		minY = Mathf.Infinity;
		maxY = -Mathf.Infinity;
		playercount = 0;
		foreach (GameObject player in players) {
			//hack break, hits errors without, needs investigating
			if (player == null) continue;
			playercount++;
			BoxCollider2D tempPlayer = player.GetComponent<BoxCollider2D>();
			//X Bounds
			if (tempPlayer.bounds.min.x < minX)
				minX = tempPlayer.bounds.min.x;
			if (tempPlayer.bounds.max.x > maxX)
				maxX = tempPlayer.bounds.max.x;
			//Y Bounds
			if (tempPlayer.bounds.min.y < minY)
				minY = tempPlayer.bounds.min.y;
			if (tempPlayer.bounds.max.y > maxY)
				maxY = tempPlayer.bounds.max.y;
		}
	}

	void CalculateCameraPosAndSize() {
		//Position
		Vector3 cameraCenter = Vector3.zero;
		foreach(GameObject player in players){
			//hack break, hits errors without, needs investigating
			if (player == null) continue;
			cameraCenter += player.GetComponent<BoxCollider2D>().bounds.center;
		}
		Vector3 finalCameraCenter = new Vector3(cameraCenter.x / playercount, cameraCenter.y / playercount, this.transform.position.z);
		//hacky minimum, should be on a per level basis
		if (finalCameraCenter.y < -17)
			finalCameraCenter.y = -17;
		this.transform.position = finalCameraCenter;
		//Size
		float sizeX = maxX - minX + 5;
		float sizeY = maxY - minY + 0;
		//hacky min/max, should be on a per level basis
		if (sizeX < 8)
			sizeX = 8;
		if (sizeY > 35)
			sizeY = 35;
		float camSize = (sizeX > sizeY ? sizeX : sizeY);
		this.GetComponent<Camera>().orthographicSize = camSize * 0.5f;
		
	} 
}
