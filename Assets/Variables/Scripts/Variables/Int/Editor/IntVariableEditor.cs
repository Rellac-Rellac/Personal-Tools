using UnityEditor;
namespace Rellac.ObjectVariables.Editor
{
	[CustomEditor(typeof(IntVariable))]
	public class IntVariableEditor : BaseVariableEditor
	{
		public override void UpdateValue(SerializedProperty value)
		{
			((IntVariable)target).Value = value.intValue;
		}
	}
}