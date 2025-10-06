using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor tool that drops selected objects onto the first surface below them
/// and aligns them with the surface slope.
/// </summary>
public class DropToSurface : Editor
{
    // Menu: Tools → Drop Selected Object | Shortcut: Ctrl + Alt + F
    [MenuItem("Tools/Drop Selected Object %&f")]
    static void Drop()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            // Cast a ray downward from the object to find the surface below
            if (Physics.Raycast(obj.transform.position, Vector3.down, out RaycastHit hit, 1000f))
            {
                // Allow undo in the Editor
                Undo.RecordObject(obj.transform, "Drop to Surface");

                float yOffset = 0f;
                Collider col = obj.GetComponent<Collider>();
                if (col != null)
                {
                    // Adjust so the bottom of the object touches the surface
                    yOffset = obj.transform.position.y - col.bounds.min.y;
                }

                // Move object to surface + offset
                obj.transform.position = hit.point + new Vector3(0, yOffset, 0);

                // Rotate object to match surface slope
                obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * obj.transform.rotation;
            }
        }
    }
}
