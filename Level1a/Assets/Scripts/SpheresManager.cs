using UnityEngine;

public class SpheresManager : MonoBehaviour
{
    [Header("Prefab to Instantiate")]
    // Adds a header above the variable in the Unity Inspector for better organization

    [Tooltip("Define the number of objects to instantiate")]
    // Shows a tooltip when hovering over the variable in the Inspector

    [Range(1f, 10f)]
    // Restricts the value of the variable to a range between 1 and 10 in the Inspector

    [SerializeField]
    private int SpheresCount = 0;
    // Number of spheres to instantiate, editable in the Inspector even though it's private

    [SerializeField]
    private GameObject sphereObject;
    // The prefab GameObject that will be instantiated, editable in the Inspector

    [HideInInspector]
    public string name;
    // Public variable but hidden in the Inspector; useful if you need to access it from other scripts but don't want it editable in the Inspector




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(sphereObject, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
