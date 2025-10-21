using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private float speedBackground = 0.5f;

    private Transform cameraTranform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTranform = Camera.main.transform;
        lastCameraPosition = cameraTranform.position;
    }

    private void LateUpdate()
    {
        Vector3 backgroundMovement = cameraTranform.position - lastCameraPosition;
        transform.position += new Vector3(backgroundMovement.x * speedBackground, backgroundMovement.y, 0);
        lastCameraPosition = cameraTranform.position;
    }
}
