using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemsUnderWater : MonoBehaviour
{
    [SerializeField] FloatOnWater FloatOnWater;
    [SerializeField] int dispersionForce = 5;
    [SerializeField] float spawnSpeed = 3;
    [SerializeField] GameObject[] possiblePrefab;

    private float timeSinceLastSpawn;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawnSpeed)
        {
            timeSinceLastSpawn -= spawnSpeed;
            Spawn();
        }
    }

    private void Spawn()
    {
        var possiblePrefabIndex = UnityEngine.Random.Range(0, possiblePrefab.Length);
        var prefab = possiblePrefab[possiblePrefabIndex];

        var x = UnityEngine.Random.Range(-50, 50);
        var y = UnityEngine.Random.Range(-30, -10);
        var z = UnityEngine.Random.Range(-50, 50);
        var randomPos = new Vector3(x, y, z);

        var newItem = Instantiate(prefab, randomPos, Quaternion.identity);
        if (newItem.GetComponent<Rigidbody>() is Rigidbody rb)
        {
            var xForce = UnityEngine.Random.Range(-dispersionForce, dispersionForce);
            var zForce = UnityEngine.Random.Range(-dispersionForce, dispersionForce);
            var force = new Vector3(xForce, 0, zForce);
            rb.AddForce(force);
        }

        if (newItem.GetComponent<PickableItem>() is PickableItem pickableItem)
        {
            pickableItem.InitPrefab(prefab);
        }

        FloatOnWater.AddInFloatingWater(newItem);
    }
}
