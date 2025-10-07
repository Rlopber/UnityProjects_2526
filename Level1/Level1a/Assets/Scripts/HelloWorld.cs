using UnityEngine;
using UnityEngine.InputSystem;

public class HelloWorld : MonoBehaviour
{
    public enum LevelDifficulty
    {
        Easy,
        Normal,
        Hard
    }
    public LevelDifficulty level;

    public int punctuation;
    public float speed;
    public bool endGame;
    public string nameGame;
    public int lifes;

    private Camera mainCam;

    private void Awake()
    {
        switch (level) 
        { 
            case LevelDifficulty.Easy:
                speed = 3f;
                break;

            case LevelDifficulty.Normal:
                speed = 5f;
                break;
            case LevelDifficulty.Hard:
                speed = 10f;
                break;
            default:
                speed = 1f;
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello World!");
        mainCam = Camera.main;
    }

    private void OnMouseDown()
    {
        lifes--;
        Debug.Log("Click detected! Lifes remaining: " + lifes);

        if (lifes == 0)
        {
            Debug.Log("Game Over! You have no lives left.");
            Destroy(gameObject);
        }
        else if (lifes == 1)
        {
            Debug.Log("Warning: Only 1 life remaining!");
        }
        else {
            Debug.Log("Ouch! You lost a life.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time: " + Time.time);
    }


    ////////////// NEW TYPE OF INPUTS //////////////

    //public void OnClick(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        // Posición del cursor en pantalla
    //        Vector2 mousePos = Mouse.current.position.ReadValue();
    //        Ray ray = mainCam.ScreenPointToRay(mousePos);

    //        // Raycast
    //        if (Physics.Raycast(ray, out RaycastHit hit))
    //        {
    //            // Comprobamos que el objeto clicado es este GameObject
    //            if (hit.collider.gameObject == gameObject)
    //            {
    //                lifes--;
    //                Debug.Log("Click on the ball! Lifes remaining: " + lifes);
    //            }
    //        }
    //    }
    //}
}
