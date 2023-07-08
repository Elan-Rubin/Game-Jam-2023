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

    // Rigidbody2D component of the game object
    private Rigidbody2D rb;
    // Vector2 representing the movement input
    private Vector2 movement;
    private Vector2 position;
    [HideInInspector] public Vector2 Position { get { return position; } }

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
        // Move the game object in the direction of the movement vector at the movement speed
        rb.velocity = movement * movementSpeed;
    }
}
