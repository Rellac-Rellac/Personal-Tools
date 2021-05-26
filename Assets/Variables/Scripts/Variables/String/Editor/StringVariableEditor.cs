using UnityEditor;
namespace Rellac.ObjectVariables.Editor
{
	[CustomEditor(typeof(StringVariable))]
	public class StringVariableEditor : BaseVariableEditor
	{
		public override void UpdateValue(SerializedProperty value)
		{
			((StringVariable)target).Value = value.stringValue;
		}
	}
}