using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HitBoxManager))]
public class HitBoxInspector : Editor {
	
	public override void OnInspectorGUI () {
		serializedObject.Update();
		HitBoxList.Show(serializedObject.FindProperty("neutralHeavy"));
		serializedObject.ApplyModifiedProperties();
	}
	
}