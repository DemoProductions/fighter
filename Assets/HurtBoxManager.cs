using UnityEngine;
using System.Collections;

public class HurtBoxManager : MonoBehaviour {
	
	// Set these in the editor
	public PolygonCollider2D neutralHeavyFrame1;
	public PolygonCollider2D neutralHeavyFrame2;
	public PolygonCollider2D neutralHeavyFrame3;
	public PolygonCollider2D neutralHeavyFrame4;
	public PolygonCollider2D neutralHeavyFrame5;
	public PolygonCollider2D neutralHeavyFrame6;
	
	// Used for organization
	private PolygonCollider2D[] colliders;
	
	// Collider on this game object
	private PolygonCollider2D localCollider;
	
	// We say box, but we're still using polygons.
	public enum hurtBoxes
	{
		neutralHeavyFrame1Box,
		neutralHeavyFrame2Box,
		neutralHeavyFrame3Box,
		neutralHeavyFrame4Box,
		neutralHeavyFrame5Box,
		neutralHeavyFrame6Box,
		clear // special case to remove all boxes
	}
	
	void Start()
	{
		// Set up an array so our script can more easily set up the hit boxes
		colliders = new PolygonCollider2D[]{neutralHeavyFrame1, neutralHeavyFrame2, neutralHeavyFrame3, neutralHeavyFrame4, neutralHeavyFrame5, neutralHeavyFrame6};
		
		// Create a polygon collider
		localCollider = gameObject.AddComponent<PolygonCollider2D>();
		localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
		localCollider.pathCount = 0; // Clear auto-generated polygons
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		// do nothing, just needs to exist
	}
	
	public void setHurtBox(hurtBoxes val)
	{
		if(val != hurtBoxes.clear)
		{
			localCollider.SetPath(0, colliders[(int)val].GetPath(0));
			return;
		}
		localCollider.pathCount = 0;
	}

	public void clearHurtBox() {
		localCollider.pathCount = 0;
	}
}