using UnityEditor;
namespace Rellac.ObjectVariables.Editor
{
	[CustomEditor(typeof(Vector2Variable))]
	public class Vector2VariableEditor : BaseVariableEditor
	{
		public override void UpdateValue(SerializedProperty value)
		{
			((Vector2Variable)target).Value = value.vector2Value;
		}
	}
}