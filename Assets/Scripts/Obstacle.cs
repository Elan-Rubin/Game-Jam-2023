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
        if (Random.value > 0.5f) transform.Rotate(0, 180, 0);
        obstacleType = ObstacleType.Wobble;
        float selectVal = Random.value;
        if (selectVal < 0.33f) obstacleType = ObstacleType.Slide;
        else if (selectVal < 0.66f) obstacleType = ObstacleType.Woosh;
        switch (obstacleType)
        {
            case ObstacleType.Wobble:
                GetComponent<Animator>().SetTrigger("Wobble");
                break;
            case ObstacleType.Woosh:
                GetComponent<Animator>().SetTrigger("Woosh");
                break;
            case ObstacleType.Slide:
                transform.position -= Vector3.one * 3;
                GetComponent<Animator>().SetTrigger("Slide");
                break;
        }
    }
    /*IEnumerator Woosh()
    {
        /*transform.DOMove(transform.position + new Vector3(Random.Range(-5f, 5f), 0, 0), 1.5f);
        yield return null;
        //i messed this up
    }
    IEnumerator Slide()
    {
        /*transform.DOMove(transform.position + new Vector3(6, 0, 0), 1f).OnComplete(() =>
        {
            transform.DOMove(transform.position + new Vector3(-6, 0, 0), 2f).OnComplete(() =>
            {
                StartCoroutine(nameof(Slide));
            });
        });
        yield return null;
    }*/
}