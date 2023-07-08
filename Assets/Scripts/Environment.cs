using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [Tooltip("Adjust descent speed")]
    [SerializeField] float speedmultiplier;
    [Tooltip("How much the player's position effects the speed")]
    [SerializeField] float playerPositionWeighting;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //the player should descend faster when closer to the bottom of the screen
        float playerPositionMultiplier = (7 - player.transform.position.y) * playerPositionWeighting;
        //move this gameobject upwards each frame
        transform.position += Vector3.up * speedmultiplier * playerPositionMultiplier * Time.deltaTime;
    }
}
