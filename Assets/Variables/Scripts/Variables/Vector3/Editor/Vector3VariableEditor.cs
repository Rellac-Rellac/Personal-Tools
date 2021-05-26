using UnityEditor;
namespace Rellac.ObjectVariables.Editor
{
	[CustomEditor(typeof(Vector3Variable))]
	public class Vector3VariableEditor : BaseVariableEditor
	{
		public override void UpdateValue(SerializedProperty value)
		{
			((Vector3Variable)target).Value = value.vector3Value;
		}
	}
}