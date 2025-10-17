using UnityEngine;

public class ShowIfAttribute : PropertyAttribute
{
    public string ConditionField;
    public object CompareValue;

    public ShowIfAttribute(string conditionField, object compareValue)
    {
        ConditionField = conditionField;
        CompareValue = compareValue;
    }
}