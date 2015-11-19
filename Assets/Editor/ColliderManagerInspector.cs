using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColliderManager), true)]
public class ColliderManagerInspector : Editor {
	
	public override void OnInspectorGUI () {
		serializedObject.Update();
		ColliderManagerList.Show(serializedObject.FindProperty("idle"));
		ColliderManagerList.Show(serializedObject.FindProperty("run"));
                ColliderManagerList.Show(serializedObject.FindProperty("jump"));
                ColliderManagerList.Show(serializedObject.FindProperty("fall"));
                ColliderManagerList.Show(serializedObject.FindProperty("land"));
		ColliderManagerList.Show(serializedObject.FindProperty("neutralLight"));
		ColliderManagerList.Show(serializedObject.FindProperty("neutralHeavy"));
		serializedObject.ApplyModifiedProperties();
	}
	
}
