using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region singleton
    private static PlayerMovement _instance;

    public static PlayerMovement Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        // Get the Rigidbody2D component of the game object
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion
    // Speed of the game object during normal movement
    public float movementSpeed = 10.0f;

    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Rigidbody2D component of the game object
    private Rigidbody2D rb;
    // Vector2 representing the movement input
    private Vector2 movement;
    private Vector2 position;
    [HideInInspector] public Vector2 Position { get { return position; } }

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    private void FixedUpdate()
    {
        Movement();
        position = transform.position;
    }

    private void Movement()
    {
        // Get the movement input as a Vector2 using Input.GetAxis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        Vector2 newPos = transform.position;
        newPos += movement * Time.deltaTime * movementSpeed;
        newPos.x = Mathf.Clamp(newPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        newPos.y = Mathf.Clamp(newPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = newPos;
    }
}
