using UnityEngine;
using UnityEngine.Events;
namespace Rellac.Mechanics
{
	/// <summary>
	/// Base State Machine class to extend a state machine for automatic functionality
	/// </summary>
	/// <typeparam name="T">Enum to be used for states</typeparam>
	public class BaseStateMachine<T> : ScriptableObject
	{
		/// <summary>
		/// Passes new state when triggered
		/// </summary>
		public TEvent onStateBegin;
		/// <summary>
		/// Passes previous state when a new one is triggered
		/// </summary>
		public TEvent onStateEnd;

		[SerializeField]
		private T state_;
		/// <summary>
		/// Current working state
		/// </summary>
		public T state
		{
			get
			{
				return state_;
			}
			set
			{
#if UNITY_EDITOR
				if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#endif
				{
					onStateBegin.Invoke(value);
					onStateEnd.Invoke(state_);
				}
				state_ = value;
			}
		}

		/// <summary>
		/// Set to a new state
		/// </summary>
		/// <param name="newState">string name of state</param>
		public void SetState(string newState)
		{
			SetState((T)System.Enum.Parse(typeof(T), newState));
		}

		/// <summary>
		/// Set to a new state
		/// </summary>
		/// <param name="newState">state to switch to</param>
		public void SetState(T newState)
		{
			state = newState;
		}
		[System.Serializable]
		public class TEvent : UnityEvent<T>
		{
		}
	}
}