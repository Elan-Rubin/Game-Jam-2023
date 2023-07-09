using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private int counter;
    [SerializeField] private LineRenderer lineRenderer;

    [HideInInspector] public Vector2 Position { get { return position; } }
    private void Start()
    {
        //lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        Movement();
        position = transform.position;
    }
    private void Update()
    {
        //var positions = new Vector3[lineRenderer.positionCount];
        //lineRenderer.GetPositions(positions);
        //var newPositions = new Vector3[positions.Length + 1];
        //for (int i = 0; i < positions.Length; i++)
        //{
        //    newPositions[i] = positions[i];
        //}
        //newPositions[newPositions.Length - 1] = transform.position;
        ////positions[positions.Length-1] = transform.position;
        //lineRenderer.SetPositions(newPositions);

        //for (int i = 0; i < lineRenderer.positionCount; i++)
        //{
        //    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i));
        //}

        counter++;
        if (counter % 10 == 0)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount -1, transform.GetChild(0).position - Environment.Instance.TotalOffset);
            Debug.Log(transform.GetChild(0).position);
        }
        
    }
    //public void UpdateString()
    //{
    //    Vector3[] newList = new Vector3[lineRenderer.positionCount];
    //    lineRenderer.GetPositions(newList);
    //    var offset = Environment.Instance.Movement;
    //    var newestList = new List<Vector3>();
    //    foreach (var x in newList)
    //    {
    //        newestList.Add(x + offset);
    //    }
    //    lineRenderer.SetPositions(newestList.ToArray());
    //}

    private void Movement()
    {
        // Get the movement input as a Vector2 using Input.GetAxis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        // Move the game object in the direction of the movement vector at the movement speed
        rb.velocity = movement * movementSpeed;
    }
}
