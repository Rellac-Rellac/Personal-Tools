using UnityEditor;
namespace Rellac.ObjectVariables.Editor
{
	[CustomEditor(typeof(FloatVariable))]
	public class FloatVariableEditor : BaseVariableEditor
	{
		public override void UpdateValue(SerializedProperty value)
		{
			((FloatVariable)target).Value = value.floatValue;
		}
	}
}