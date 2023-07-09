using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core.Environments;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [Tooltip("Adjust descent speed")]
    [SerializeField] float speedmultiplier;
    [Tooltip("How much the player's position effects the speed")]
    [SerializeField] float playerPositionWeighting;

    private GameObject player;
    private Vector3 movement;
    public Vector3 Movement { get { return movement; } }

    private Vector3 totalOffset = Vector3.zero;
    public Vector3 TotalOffset { get {  return totalOffset; } }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #region singleton
    private static Environment _instance;

    public static Environment Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    #endregion
    private void Update()
    {
        //the player should descend faster when closer to the bottom of the screen
        float playerPositionMultiplier = (7 - player.transform.position.y) * playerPositionWeighting;
        //move this gameobject upwards each frame
        movement = Vector3.up * speedmultiplier * playerPositionMultiplier * Time.deltaTime;
        transform.position += movement;
        totalOffset += movement;
        //PlayerMovement.Instance.UpdateString();
    }
}
