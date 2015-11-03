using UnityEngine;
using System.Collections;

public class HitBoxManager : MonoBehaviour {
	
	// Set these in the editor
	public PolygonCollider2D[] neutral;
	public PolygonCollider2D[] neutralLight;
	public PolygonCollider2D[] neutralHeavy;
	
	// Collider on this game object
	private PolygonCollider2D localCollider;
	
	public enum types
	{
		neutral,
		neutral_light,
		neutral_heavy,
		clear // special case to remove all boxes
	}
	
	// We say box, but we're still using polygons.
	public enum frames
	{
		frame1,
		frame2,
		frame3,
		frame4,
		frame5,
		frame6,
		frame7,
		frame8,
		frame9,
		frame10,
		clear // special case to remove all boxes
	}

	void Start()
	{
		// Set up an array so our script can more easily set up the hit boxes
//		colliders = new PolygonCollider2D[]{kickframe1, kickframe2, kickframe3, kickframe4, kickframe5, kickframe6};
		
		// Create a polygon collider
		localCollider = gameObject.AddComponent<PolygonCollider2D>();
		localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
		localCollider.pathCount = 0; // Clear auto-generated polygons
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		string reaction; //debug var
		// ignore parent collisions
		if (collider.gameObject.name == this.transform.parent.name) {
			Debug.Log ("parent collision ignored");
			return;
		}

		// debug, loop, should simplify to simply the if statement and its function call
		if (collider.gameObject.name.Contains ("HurtBox") && collider.transform.parent.name != this.transform.parent.name) {
			reaction = "hit";
			GetComponentInParent<PlayerMovement> ().hit (collider);
		} else {
			reaction = "ignored";
		}
		Debug.Log (this.gameObject.name + " on " + this.transform.parent.name + " hit " + collider.gameObject.name + ": " + reaction);
	}
	
	public void setHitBox(types type, frames frame)
	{
		if(frame != frames.clear)
		{
			switch ((int)type){
			case 2:
				localCollider.SetPath(0, neutralHeavy[(int)frame].GetPath(0));
				break;
			default:
				localCollider.SetPath(0, neutralHeavy[(int)frame].GetPath(0));
				break;
			}
			return;
		}
		localCollider.pathCount = 0;
	}

	public void clearHitBox() {
		localCollider.pathCount = 0;
	}
}