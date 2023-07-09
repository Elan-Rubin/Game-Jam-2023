using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleType obstacleType;
    private enum ObstacleType
    {
        Wobble,
        Woosh, //over to one side
        Slide,//back and forth
    }
    void Start()
    {
        obstacleType = ObstacleType.Wobble;
        if (Random.value > 0.8f) obstacleType = ObstacleType.Slide;
        if (Random.value > 0.8f) obstacleType = ObstacleType.Woosh;
        switch (obstacleType)
        {
            case ObstacleType.Wobble:
                GetComponent<Animator>().enabled = true;
                break;
            case ObstacleType.Woosh:
                StartCoroutine(nameof(Woosh));
                break;
            case ObstacleType.Slide:
                transform.position -= Vector3.one * 3;
                StartCoroutine(nameof(Slide));
                break;
        }
    }
    IEnumerator Woosh()
    {
        transform.DOMove(transform.position + new Vector3(Random.Range(-5f, 5f), 0, 0), 1.5f);
        yield return null;
        //i messed this up
    }
    IEnumerator Slide()
    {
        transform.DOMove(transform.position + new Vector3(6, 0, 0), 1.5f).OnComplete(() =>
        {
            transform.DOMove(transform.position + new Vector3(-6, 0, 0), 3f).OnComplete(() =>
            {
                StartCoroutine(nameof(Slide));
            });
        });
        yield return null;
    }
}