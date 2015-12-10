using UnityEngine;
using System.Collections;

public class ColliderManager : MonoBehaviour {
	
	// Set these in the editor
	public PolygonCollider2D[] idle;
	public PolygonCollider2D[] run;
	public PolygonCollider2D[] jump;
	public PolygonCollider2D[] fall;
	public PolygonCollider2D[] land;
	public PolygonCollider2D[] neutralLight;
	public PolygonCollider2D[] neutralHeavy;
	
	// Collider on this game object
	private PolygonCollider2D localCollider;
	
	public enum types
	{
		idle,
		run,
		jump,
		fall,
		land,
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
		frame11,
		frame12,
		frame13,
		frame14,
		clear // special case to remove all boxes
	}
	
	void Start()
	{
		// Create a polygon collider
		localCollider = gameObject.AddComponent<PolygonCollider2D>();
		localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
		localCollider.pathCount = 0; // Clear auto-generated polygons
	}
	
	public virtual void OnTriggerEnter2D(Collider2D collider)
	{

	}
	
	public void setCollider(types type, frames frame)
	{
		if(frame != frames.clear)
		{
			switch ((int)type){
			case (int)types.idle:
				localCollider.SetPath(0, idle[(int)frame].GetPath(0));
				break;
			case (int)types.run:
				localCollider.SetPath(0, run[(int)frame].GetPath(0));
				break;
			case (int)types.jump:
				localCollider.SetPath(0, jump[(int)frame].GetPath(0));
				break;
			case (int)types.fall:
				localCollider.SetPath(0, fall[(int)frame].GetPath(0));
				break;
			case (int)types.land:
				localCollider.SetPath(0, land[(int)frame].GetPath(0));
				break;
			case (int)types.neutral_light:
				localCollider.SetPath(0, neutralLight[(int)frame].GetPath(0));
				break;
			default:
				localCollider.SetPath(0, neutralHeavy[(int)frame].GetPath(0));
				break;
			}
			return;
		}
		localCollider.pathCount = 0;
	}
	
	public void clear() {
		localCollider.pathCount = 0;
	}
}
