using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public bool canSpawnPerson = true;
    public float minTimeBetweenSpawns = 1;
    public float maxTimeBetweenSpawns = 8;
    [SerializeField] GameObject personPrefab;
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
        yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
        Vector2 instantiatePos = transform.position;
        instantiatePos.x = Random.Range(-7f, 7f);
        GameObject newPerson = Instantiate(personPrefab, instantiatePos, Quaternion.identity);
        newPerson.transform.parent = objectTransform;
        canSpawnPerson = true;
    }
}
