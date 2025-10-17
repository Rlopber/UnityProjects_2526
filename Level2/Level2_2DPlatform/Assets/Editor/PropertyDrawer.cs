using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute), true)] // true = incluye ShowHeaderIfAttribute
public class ShowIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProp = property.serializedObject.FindProperty(showIf.ConditionField);

        bool shouldShow = false;

        if (conditionProp != null)
        {
            switch (conditionProp.propertyType)
            {
                case SerializedPropertyType.Enum:
                    shouldShow = conditionProp.enumValueIndex.Equals((int)showIf.CompareValue);
                    break;
                case SerializedPropertyType.Boolean:
                    shouldShow = conditionProp.boolValue.Equals(showIf.CompareValue);
                    break;
            }
        }

        if (!shouldShow) return;

        // Si es ShowHeaderIfAttribute, dibuja el header
        if (showIf is ShowHeaderIfAttribute headerIf)
        {
            EditorGUI.LabelField(position, headerIf.Header, EditorStyles.boldLabel);
            position.y += EditorGUIUtility.singleLineHeight + 2;
        }

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProp = property.serializedObject.FindProperty(showIf.ConditionField);

        bool shouldShow = false;

        if (conditionProp != null)
        {
            switch (conditionProp.propertyType)
            {
                case SerializedPropertyType.Enum:
                    shouldShow = conditionProp.enumValueIndex.Equals((int)showIf.CompareValue);
                    break;
                case SerializedPropertyType.Boolean:
                    shouldShow = conditionProp.boolValue.Equals(showIf.CompareValue);
                    break;
            }
        }

        if (!shouldShow) return 0f;

        float height = EditorGUI.GetPropertyHeight(property, label, true);
        if (showIf is ShowHeaderIfAttribute)
            height += EditorGUIUtility.singleLineHeight + 2; // espacio para el header
        return height;
    }
}
