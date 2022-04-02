using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNextItem : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject itemPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
            if (item.GetComponent<Rigidbody>() is Rigidbody rb)
            {
                rb.AddForce(spawnPoint.forward * 275);
            }
        }
    }
}
