using UnityEngine;
using System.Collections;

public class HitBoxManager : MonoBehaviour {
	
	// Set these in the editor
	public PolygonCollider2D kickframe1;
	public PolygonCollider2D kickframe2;
	public PolygonCollider2D kickframe3;
	public PolygonCollider2D kickframe4;
	public PolygonCollider2D kickframe5;
	public PolygonCollider2D kickframe6;
	
	// Used for organization
	private PolygonCollider2D[] colliders;
	
	// Collider on this game object
	private PolygonCollider2D localCollider;
	
	// We say box, but we're still using polygons.
	public enum hitBoxes
	{
		kickframe1Box,
		kickframe2Box,
		kickframe3Box,
		kickframe4Box,
		kickframe5Box,
		kickframe6Box,
		clear // special case to remove all boxes
	}
	
	void Start()
	{
		// Set up an array so our script can more easily set up the hit boxes
		colliders = new PolygonCollider2D[]{kickframe1, kickframe2, kickframe3, kickframe4, kickframe5, kickframe6};
		
		// Create a polygon collider
		localCollider = gameObject.AddComponent<PolygonCollider2D>();
		localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
		localCollider.pathCount = 0; // Clear auto-generated polygons
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("Collider hit something!");
		GetComponentInParent<PlayerMovement> ().hit (collider);
	}
	
	public void setHitBox(hitBoxes val)
	{
		if(val != hitBoxes.clear)
		{
			localCollider.SetPath(0, colliders[(int)val].GetPath(0));
			return;
		}
		localCollider.pathCount = 0;
	}

	public void clearHitBox() {
		localCollider.pathCount = 0;
	}
}