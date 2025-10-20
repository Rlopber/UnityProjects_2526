using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

[InitializeOnLoad]
public class AutoSaveEditor
{
    private static double nextSaveTime;
    private static double saveInterval = 300; // default 5 minutos
    private static bool autoSaveEnabled = true;

    // Inicializa al cargar Unity
    static AutoSaveEditor()
    {
        LoadPreferences();
        EditorApplication.update += Update;
    }

    private static void Update()
    {
        if (!autoSaveEnabled) return;

        if (EditorApplication.timeSinceStartup >= nextSaveTime)
        {
            SaveProjectIfDirty();
            nextSaveTime = EditorApplication.timeSinceStartup + saveInterval;
        }
    }

    private static void SaveProjectIfDirty()
    {
        bool anySaved = false;

        // Guarda escenas abiertas solo si están modificadas
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            var scene = EditorSceneManager.GetSceneAt(i);
            if (scene.isDirty)
            {
                EditorSceneManager.SaveScene(scene);
                anySaved = true;
            }
        }

        // Guarda assets modificados
        if (AssetDatabase.IsOpenForEdit(AssetDatabase.GetAllAssetPaths()[0])) // simple check
        {
            AssetDatabase.SaveAssets();
            anySaved = true;
        }

        if (anySaved)
        {
            Debug.Log("[AutoSave] Project saved at " + DateTime.Now.ToString("HH:mm:ss"));
        }
    }

    // Menú para controlar el autosave
    [MenuItem("Tools/AutoSave/Toggle AutoSave")]
    private static void ToggleAutoSave()
    {
        autoSaveEnabled = !autoSaveEnabled;
        EditorPrefs.SetBool("AutoSaveEnabled", autoSaveEnabled);
        Debug.Log("[AutoSave] AutoSave " + (autoSaveEnabled ? "Enabled" : "Disabled"));
    }

    [MenuItem("Tools/AutoSave/Set Interval...")]
    private static void SetInterval()
    {
        string input = EditorUtility.DisplayDialogComplex("Set AutoSave Interval",
            "Enter interval in minutes:", "OK", "Cancel", "") == 0 ?
            EditorUtility.DisplayDialogComplex("Interval", "Minutes", "OK", "Cancel", "").ToString() : null;

        if (!string.IsNullOrEmpty(input) && float.TryParse(input, out float minutes))
        {
            saveInterval = minutes * 60;
            EditorPrefs.SetFloat("AutoSaveInterval", (float)saveInterval);
            Debug.Log("[AutoSave] Interval set to " + minutes + " minutes");
        }
    }

    private static void LoadPreferences()
    {
        autoSaveEnabled = EditorPrefs.GetBool("AutoSaveEnabled", true);
        saveInterval = EditorPrefs.GetFloat("AutoSaveInterval", 300f);
        nextSaveTime = EditorApplication.timeSinceStartup + saveInterval;
    }
}
