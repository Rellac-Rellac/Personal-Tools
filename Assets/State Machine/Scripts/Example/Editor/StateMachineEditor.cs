using UnityEditor;
using Rellac.Mechanics.Editor;
namespace ExampleStateMachine.Editor
{
	/// <summary>
	/// Example editor - inherits from the BaseStateMachineEditor class and passes in the State enum we're using
	/// </summary>
	[CustomEditor(typeof(StateMachine))]
	public class StateMachineEditor : BaseStateMachineEditor<State>{}
}