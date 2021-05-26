using UnityEngine;
using Rellac.Mechanics;
namespace ExampleStateMachine
{
	/// <summary>
	/// Example state machine - inherits from the BaseStateMachine class and passes in the State enum we're using
	/// Functionality of the whole state machine is provided with the assigned enum
	/// The state machine can switch to any state, even in the editor for debugging
	/// </summary>
	[CreateAssetMenu(fileName = "New State Machine", menuName = "Mechanics/State Machine")]
	public class StateMachine : BaseStateMachine<State> {}

	/// <summary>
	/// Enum denoting states that this machine will pass through
	/// </summary>
	public enum State
	{
		A,
		B
	}
}