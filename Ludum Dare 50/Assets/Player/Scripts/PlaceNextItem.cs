using Assets.GameManagement;
using Assets.Inventory.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNextItem : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    private IInventoryBag inventoryBag;

    private void Start()
    {
        inventoryBag = GameManager.Instance.InventoryBag;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var itemPrefab = inventoryBag.Use();
            var item = Instantiate(itemPrefab, spawnPoint.position, Random.rotation);
            if (item.GetComponent<Rigidbody>() is Rigidbody rb)
            {
                rb.AddForce(spawnPoint.forward * 350);
                rb.AddTorque(Random.rotation.eulerAngles);
            }
        }
    }
}
