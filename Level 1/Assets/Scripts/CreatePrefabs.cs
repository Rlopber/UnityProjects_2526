using UnityEngine;

public class CreatePrefabs : MonoBehaviour
{

    public GameObject spherePrefab;
    public int sphereNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float[] scales = { 0.5f, 2.8f, 4f, 7f };

        for (int i = 1; i <= sphereNumber; i++)
        {
            float randomScale = Random.Range(0, scales.Length);
            Vector3 initialPosition = new Vector3(1 * i, 1 * i, 1 * i);
            Instantiate(spherePrefab, initialPosition, Quaternion.identity).transform.localScale = new Vector3 (randomScale, randomScale, randomScale);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
