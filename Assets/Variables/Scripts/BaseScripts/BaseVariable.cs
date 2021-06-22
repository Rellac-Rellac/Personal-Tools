using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Rellac.ObjectVariables
{
	/// <summary>
	/// Extension class to provide Variable functionality with provided type
	/// </summary>
	/// <typeparam name="T">Serialisable type to store in this variable</typeparam>
	public class BaseVariable<T> : ScriptableObject
	{
		[HideInInspector]
		[SerializeField]
		private T value_;
		public T Value
		{
			get
			{
				return value_;
			}
			set
			{
				// Don't bother if it's the exact same value
				if (EqualityComparer<T>.Default.Equals(value_, value)) return;

				// set new value
				value_ = value;

				// in editor mode, the event will only fire during play
#if UNITY_EDITOR || UNITY_EDITOR_LINUX || UNITY_EDITOR_OSX || UNITY_EDITOR_WIN
				if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#endif
				{
					onValueModified.Invoke(value);
				}
			}
		}
		/// <summary>
		/// Passes new value on modification
		/// </summary>
		public TEvent onValueModified = new TEvent();

#if UNITY_EDITOR || UNITY_EDITOR_LINUX || UNITY_EDITOR_OSX || UNITY_EDITOR_WIN || UNITY_EDITOR_ANDROID
		/// <summary>
		/// Description for inspector
		/// Doesn't exist outside of the editor!
		/// </summary>
		[Tooltip("Description for inspector")]
		[TextArea]
		public string developerDescription;
#endif
		public void Modify(BaseVariable<T> value)
		{
			Modify(value.Value);
		}
		public void Modify(T value)
		{
			dynamic a = Value;
			dynamic b = value;
			SetValue(a + b);
		}
		public void SetValue(T value)
		{
			Value = value;
		}
		[System.Serializable]
		public class TEvent : UnityEvent<T> { }
	}
}