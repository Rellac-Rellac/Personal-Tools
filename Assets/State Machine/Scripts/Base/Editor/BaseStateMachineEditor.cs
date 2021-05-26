using System;
using UnityEditor;
using UnityEngine;

namespace Rellac.Mechanics.Editor
{
	/// <summary>
	/// Base Editor class to extend a state machine editor for automatic functionality
	/// </summary>
	/// <typeparam name="T">Enum to be used for states</typeparam>
	public class BaseStateMachineEditor<T> : UnityEditor.Editor
	{
		SerializedProperty onStateBegin;
		SerializedProperty onStateEnd;
		SerializedProperty state_;

		void OnEnable()
		{
			onStateBegin = serializedObject.FindProperty("onStateBegin");
			onStateEnd = serializedObject.FindProperty("onStateEnd");
			state_ = serializedObject.FindProperty("state_");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			BaseStateMachine<T> main = (BaseStateMachine<T>)target;
			StateField(main.state);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(onStateBegin);
			EditorGUILayout.PropertyField(onStateEnd);
			serializedObject.ApplyModifiedProperties();
		}

		/// <summary>
		/// Find the type and values of an enum and create a popup from that info
		/// </summary>
		/// <param name="state">state found</param>
		private void StateField(T state)
		{
			var enumValues = Enum.GetValues(typeof(T));
			string[] entries = new string[enumValues.Length];
			for (int i = 0; i < enumValues.Length; i++)
			{
				entries[i] = enumValues.GetValue(i).ToString();
			}
			state_.enumValueIndex = EditorGUILayout.Popup(new GUIContent("State", "Currently selected state"), state_.enumValueIndex, entries);
		}
	}
}