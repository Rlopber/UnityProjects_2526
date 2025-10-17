using UnityEngine;
using UnityEditor;

/// <summary>
/// Hierarchy Window Group Header with subcategory colors
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

        // Etiqueta principal: ---Nombre---
        if (name.StartsWith("---"))
        {
            EditorGUI.DrawRect(selectionRect, new Color(0.4f, 0f, 0.6f));
            EditorGUI.DropShadowLabel(selectionRect, name.Replace("-", "").ToUpperInvariant());
        }
        // Subcategoría: --Nombre--
        else if (name.StartsWith("--"))
        {
            EditorGUI.DrawRect(selectionRect, new Color(0.6f, 0.3f, 0.9f));
            EditorGUI.DropShadowLabel(selectionRect, "   " + name.Replace("-", "").ToUpperInvariant());
        }
        // Opcional: más niveles de subcategoría
        else if (name.StartsWith("-"))
        {
            EditorGUI.DrawRect(selectionRect, new Color(0.7f, 0.5f, 1f));
            EditorGUI.DropShadowLabel(selectionRect, "      " + name.Replace("-", "").ToUpperInvariant());
        }
    }
}
