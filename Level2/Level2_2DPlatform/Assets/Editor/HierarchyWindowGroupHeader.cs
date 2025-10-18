using UnityEngine;
using UnityEditor;

/// <summary>
/// Hierarchy Window Group Header with subcategory colors and custom shadowed text
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGroupHeader
{
    static HierarchyWindowGroupHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (gameObject == null) return;

        string name = gameObject.name;

        // Ajustar indentación según subnivel
        string displayName = name.Replace("-", "").ToUpperInvariant();
        if (name.StartsWith("--"))
            displayName = "   " + displayName;
        else if (name.StartsWith("-"))
            displayName = "      " + displayName;

        // Selección de color según nivel
        Color bgColor = Color.clear;
        if (name.StartsWith("---")) bgColor = new Color(0.4f, 0f, 0.6f);
        else if (name.StartsWith("--")) bgColor = new Color(0.6f, 0.3f, 0.9f);
        else if (name.StartsWith("-")) bgColor = new Color(0.7f, 0.5f, 1f);

        if (bgColor != Color.clear)
        {
            EditorGUI.DrawRect(selectionRect, bgColor);

            // Crear estilo para centrar texto
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
            };

            // Rect centrado verticalmente
            Rect textRect = new Rect(
                selectionRect.x,
                selectionRect.y,
                selectionRect.width,
                selectionRect.height
            );

            // Dibujar sombra: texto negro desplazado 1px
            Rect shadowRect = textRect;
            shadowRect.position += new Vector2(1f, 1f);
            style.normal.textColor = Color.black;
            GUI.Label(shadowRect, displayName, style);

            // Dibujar texto encima: color blanco
            style.normal.textColor = Color.white;
            GUI.Label(textRect, displayName, style);
        }
    }
}
