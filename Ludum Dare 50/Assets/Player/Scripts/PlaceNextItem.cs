using Assets.GameManagement;
using Assets.Inventory.Scripts;
using Assets.SharedScripts;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNextItem : MonoBehaviour
{
    [SerializeField] Transform playerSpawnCenter;
    [SerializeField] Transform spawnPoint;
    private IInventoryBag inventoryBag;
    private float spawnPointDistance;

    public bool IsThrowing { get; private set; }

    private bool _isThrowingSignal;

    private void Start()
    {
        inventoryBag = GameManager.Instance.InventoryBag;
        spawnPointDistance = (playerSpawnCenter.position - spawnPoint.position).magnitude;
    }

    private void Update()
    {
        FollowMousePos();
        Throw();
    }

    private void FollowMousePos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var combileLayerMask = KnownedLayers.GetTrowableSurfacesLayerMask();
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance: 50f, combileLayerMask))
        {
            var direction = raycastHit.point - playerSpawnCenter.position;
            spawnPoint.rotation = GetNewSpawnRotation(direction);
            spawnPoint.position = GetNewSpawnPos(raycastHit, direction);
        }
    }

    private Quaternion GetNewSpawnRotation(Vector3 direction)
    {
        var euler = spawnPoint.rotation.eulerAngles;
        euler.y = Quaternion.LookRotation(direction).eulerAngles.y;
        return Quaternion.Euler(euler);
    }

    private Vector3 GetNewSpawnPos(RaycastHit raycastHit, Vector3 direction)
    {

        var clampPos = Vector3.ClampMagnitude(direction, spawnPointDistance);

        var spawnPointNewPos = playerSpawnCenter.position + clampPos;
        spawnPointNewPos.y = spawnPoint.position.y;
        return spawnPointNewPos;
    }

    private void Throw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var itemPrefab = inventoryBag.PickNext();
            if (itemPrefab == null)
            {
                return;
            }

            var item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
            if (item.GetComponent<Rigidbody>() is Rigidbody rb)
            {
                rb.AddForce(spawnPoint.forward * 350);
                rb.AddTorque(Random.rotation.eulerAngles);
                _isThrowingSignal = true;
            }

            if (item.GetComponent<PickableItem>() is PickableItem pickableItem)
            {
                pickableItem.InitPrefab(itemPrefab);
            }

            ColliderUtils.ChangeCollidersTrigger(item, true);
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
