using Assets.GameManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNextItem : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject itemPrefab;
    private object inventoryBag;

    private void Start()
    {
        inventoryBag = GameManager.Instance.InventoryBag;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var item = Instantiate(itemPrefab, spawnPoint.position, Random.rotation);
            if (item.GetComponent<Rigidbody>() is Rigidbody rb)
            {
                rb.AddForce(spawnPoint.forward * 350);
                rb.AddTorque(Random.rotation.eulerAngles);
            }
        }
    }
}
