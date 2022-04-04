using Assets.GameManagement;
using Assets.Inventory.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNextItem : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    private IInventoryBag inventoryBag;

    public bool IsThrowing { get; private set; }

    private bool _isThrowingSignal;

    private void Start()
    {
        inventoryBag = GameManager.Instance.InventoryBag;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var itemPrefab = inventoryBag.Use();
            var item = Instantiate(itemPrefab, spawnPoint.position, Random.rotation);
            if (item.GetComponent<Rigidbody>() is Rigidbody rb)
            {
                rb.AddForce(spawnPoint.forward * 350);
                rb.AddTorque(Random.rotation.eulerAngles);
                _isThrowingSignal = true;
            }
        }
    }

    private void FixedUpdate()
    {
        RaiseThrowing();
    }

    private void RaiseThrowing()
    {
        if (_isThrowingSignal && !IsThrowing)
        {
            IsThrowing = true;
        }
    }

    public void ResetIsThrowing()
    {
        if (_isThrowingSignal && IsThrowing)
        {
            _isThrowingSignal = false;
            IsThrowing = false;
        }
    }
}
