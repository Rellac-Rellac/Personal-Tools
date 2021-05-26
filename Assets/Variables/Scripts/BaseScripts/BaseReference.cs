using Rellac.ObjectVariables;
[System.Serializable]
public class BaseReference<T>
{
	/// <summary>
	/// Use a constant value or a direct input
	/// </summary>
	public bool UseConstant = true;
	/// <summary>
	/// Direct input from user
	/// </summary>
	public T ConstantValue;
	/// <summary>
	/// ScriptableObject reference
	/// </summary>
	public BaseVariable<T> Variable;
	/// <summary>
	/// float value
	/// </summary>
	public T value
	{
		get
		{
			return UseConstant ? ConstantValue : Variable.Value;
		}
	}
}
