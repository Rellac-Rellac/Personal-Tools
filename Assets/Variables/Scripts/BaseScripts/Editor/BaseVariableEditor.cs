using UnityEngine;
using UnityEditor;
using System;

namespace Rellac.ObjectVariables.Editor
{
	/// <summary>
	/// BaseVariableEditor will render the editor for a variable
	/// Extend this class and provide the source to activate
	/// UpdateValue must be overriden to know how to save the SerializedProperty data
	/// </summary>
	public class BaseVariableEditor : UnityEditor.Editor
	{
		SerializedProperty value_;
		SerializedProperty onValueModified;
		SerializedProperty developerDescription;

		void OnEnable()
		{
			value_ = serializedObject.FindProperty("value_");
			onValueModified = serializedObject.FindProperty("onValueModified");
			developerDescription = serializedObject.FindProperty("developerDescription");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(value_, new GUIContent("Value"));
			UpdateValue(value_);
			EditorGUILayout.PropertyField(developerDescription);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(onValueModified);
			serializedObject.ApplyModifiedProperties();
		}

		/// <summary>
		/// Update the target.Value with input
		/// Must be overriden in parent code
		/// Ex: ((IntVariable)target).Value = value.intValue;
		/// </summary>
		/// <param name="value"></param>
		public virtual void UpdateValue(SerializedProperty value)
		{
			throw new NotImplementedException("UpdateValue must be overriden in Editor code to successfully update a variable.");
		}
	}
}