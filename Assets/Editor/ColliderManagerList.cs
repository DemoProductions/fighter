using UnityEditor;
using UnityEngine;

public static class ColliderManagerList {
	
	public static void Show (SerializedProperty list) {
		EditorGUILayout.PropertyField(list);
		if (list.isExpanded) {
			EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
			for (int i = 0; i < list.arraySize; i++) {
				GUIContent label = new GUIContent();
				label.text = "Frame" + (i+1);
				EditorGUILayout.PropertyField (list.GetArrayElementAtIndex (i), label);
			}
		}
	}

}
