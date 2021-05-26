using UnityEditor;
using UnityEngine;
/// <summary>
/// The BaseReferenceDrawer class is a PropertyDrawer that will extend any variable reference that inherits from it
/// It will assume that the reference is using the "UseConstant", "ConstantValue" and "Variable" vars
/// This will create a foldout in the inspector to replace the default class structure Unity renders
/// </summary>
public class BaseReferenceDrawer : PropertyDrawer
{
	/// <summary>
	/// Inform if the user has selected the foldout
	/// </summary>
	private bool foldout;
	/// <summary>
	/// Extra space to render for this property
	/// </summary>
	private float extraSpace;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Initialise GUI
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// Draw Variable Reference
		OnDrawInspector(position, property, label);

		// End GUI
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	/// <summary>
	/// Draw everything in the PropertyDrawer
	/// </summary>
	/// <param name="position">Rect supplied for this field</param>
	/// <param name="property">Property to serialise</param>
	/// <param name="label">Label for this property</param>
	public virtual void OnDrawInspector(Rect position, SerializedProperty property, GUIContent label)
	{
		// Draw icon popup
		DrawIcon(position, property);
		// Draw value
		DrawSelection(position, property, label);
	}

	/// <summary>
	/// Draws the content of the CustomValue input
	/// </summary>
	/// <param name="position">Rect supplied for this field</param>
	/// <param name="property">Property to serialise</param>
	/// <param name="label">Label for this property</param>
	public virtual void OnDrawContent(Rect position, SerializedProperty property, GUIContent label)
	{
		var inputRect = new Rect(position.x + 20, position.y, position.width - 20, position.height - extraSpace);
		if (property.type.ToLower() != "string" && property.isArray)
		{
			DrawArray(property, inputRect, label.text);
		}
		else
		{
			EditorGUI.PropertyField(inputRect, property, GUIContent.none);
		}
	}

	/// <summary>
	/// Draws the value of the Variable input
	/// </summary>
	/// <param name="rect">Rect supplied for this field</param>
	/// <param name="property">Property to serialise</param>
	public virtual void OnDrawVariable(Rect rect, SerializedProperty property)
	{
		EditorGUI.PropertyField(rect, property.FindPropertyRelative("Variable"), GUIContent.none);
	}

	/// <summary>
	/// Decide whether to draw ConstantValue or Variable and render to user
	/// </summary>
	/// <param name="position">Rect supplied for this field</param>
	/// <param name="property">Property to serialise</param>
	/// <param name="label">Label for this property</param>
	public void DrawSelection(Rect position, SerializedProperty property, GUIContent label)
	{
		var useConstantProp = property.FindPropertyRelative("UseConstant");
		var inputRect = new Rect(position.x + 20, position.y, position.width - 20, position.height - extraSpace);
		// Return colours
		GUI.backgroundColor = Color.white;
		GUI.contentColor = Color.white;

		// show appropriate input
		if (useConstantProp.boolValue)
		{
			var valueProp = property.FindPropertyRelative("ConstantValue");
			OnDrawContent(position, valueProp, label);
		}
		else
		{
			OnDrawVariable(inputRect, property);
		}
	}

	/// <summary>
	/// Draw and array from CustomValue input
	/// </summary>
	/// <param name="prop">Property to serialise</param>
	/// <param name="rect">Rect supplied for this field</param>
	/// <param name="label">Label for this property</param>
	private void DrawArray(SerializedProperty prop, Rect rect, string label)
	{
		Vector3 pos = rect.position;
		EditorGUI.indentLevel++;
		rect.position = pos;
		extraSpace = 0;
		foldout = EditorGUI.Foldout(rect, foldout, label);
		if (foldout)
		{
			pos.y += 20;
			rect.position = pos;
			prop.arraySize = EditorGUI.IntField(rect, "Size", prop.arraySize);
			EditorGUI.indentLevel = 0;
			for (int i = 0; i < prop.arraySize; i++)
			{
				Rect elementRect = rect;
				pos = elementRect.position;
				pos.y += (20 * i) + 20;
				elementRect.position = pos;
				EditorGUI.PropertyField(elementRect, prop.GetArrayElementAtIndex(i), new GUIContent(""));
			}
			extraSpace = (prop.arraySize * 20) + 20;
		}
	}

	/// <summary>
	/// Draw the icon for the foldout
	/// </summary>
	/// <param name="position">Rect supplied for this field</param>
	/// <param name="property">Property to serialise</param>
	public void DrawIcon(Rect position, SerializedProperty property)
	{
		var useConstantProp = property.FindPropertyRelative("UseConstant");

		// remove background
		GUI.backgroundColor = new Color(0, 0, 0, 0);
		GUI.contentColor = new Color(0, 0, 0, 0);

		var iconRect = new Rect(position.x, position.y, 20, position.height - extraSpace);
		Texture icon = EditorGUIUtility.Load("icons/d_UnityEditor.SceneHierarchyWindow.png") as Texture2D;
		GUI.DrawTexture(iconRect, icon);


		var dropdownRect = new Rect(position.x, position.y, 20, position.height - extraSpace);
		int popup = EditorGUI.Popup(dropdownRect, useConstantProp.boolValue ? 0 : 1, new string[] { "Use Constant", "Use Variable" });
		useConstantProp.boolValue = popup == 0 ? true : false;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) + extraSpace;
	}
}