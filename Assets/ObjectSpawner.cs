using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public bool canSpawnPerson = true;
    public float baseMinTimeBetweenSpawns, finalMinTime = 1;
    public float baseMaxTimeBetweenSpawns, finalMaxTime = 8;
    [SerializeField] List<GameObject> personPrefabs = new();
    [SerializeField] Transform objectTransform;

    private void Update()
    {
        if (canSpawnPerson)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        canSpawnPerson = false;
        StartCoroutine(SpawnCooldown());
    }

    IEnumerator SpawnCooldown()
    {
        float x = GameManager.Instance.timeLeft / GameManager.Instance.timeTotal;
        yield return new WaitForSeconds(Random.Range(Mathf.Lerp(baseMinTimeBetweenSpawns, finalMinTime,x), Mathf.Lerp(baseMaxTimeBetweenSpawns, finalMaxTime, x)));
        Vector2 instantiatePos = transform.position;
        instantiatePos.x = Random.Range(-7f, 7f);
        GameObject newPerson = Instantiate(personPrefabs[Random.Range(0, personPrefabs.Count)], instantiatePos, Quaternion.identity);
        newPerson.transform.parent = objectTransform;
        canSpawnPerson = true;
    }
}
