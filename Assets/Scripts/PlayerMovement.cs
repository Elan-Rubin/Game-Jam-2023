using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
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
    private int counter;
    [SerializeField] private LineRenderer lineRenderer;

    private List<Vector3> previousPositions = new();

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
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
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.GetChild(0).position - Environment.Instance.TotalOffset);
            previousPositions.Add(transform.GetChild(0).position - Environment.Instance.TotalOffset);
            //Debug.Log(transform.GetChild(0).position);
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

        Vector2 newPos = transform.position;
        newPos += movement * Time.deltaTime * movementSpeed;
        newPos.x = Mathf.Clamp(newPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        newPos.y = Mathf.Clamp(newPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("person"))
        {
            //var list = new ContactPoint2D[0];
            //collision.GetContacts(list);

            var colpos = collision.gameObject.transform.position;
            var currpos = transform.position;

            collision.gameObject.GetComponent<Person>().SaySomething();
            var x = DOTween.Sequence();
            //CameraManager.Instance.ShakeCamera();
            CameraManager.Instance.BounceCamera();
            x.AppendInterval(0.5f);
            x.Append(collision.gameObject.transform.DOMove(Vector3.Lerp(colpos, currpos, 0.5f), 0.25f).OnComplete(() =>
            {
                collision.gameObject.transform.DOScale(Vector3.zero, 0.3f);
                //collision.gameObject.transform.parent = transform;
            }));
        }
    }
}
