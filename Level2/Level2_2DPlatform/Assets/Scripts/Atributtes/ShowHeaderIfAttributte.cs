using UnityEngine;

/// <summary>
/// Conditional header that only shows if a condition is met.
/// </summary>
public class ShowHeaderIfAttribute : ShowIfAttribute
{
    public string Header;

    public ShowHeaderIfAttribute(string header, string conditionField, object compareValue)
        : base(conditionField, compareValue)
    {
        Header = header;
    }
}