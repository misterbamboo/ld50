using Assets.GameManagement;
using Assets.Inventory.Scripts;
using Assets.SharedScripts;
using Assets.Utils;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    float distanceToBecomePickable = 2;
    bool isPickable = false;
    private Transform player;
    private IInventoryBag inventoryBag;
    private GameObject itemPrefab;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        inventoryBag = GameManager.Instance.InventoryBag;
        isPickable = false;
    }

    private void Update()
    {
        if (isPickable) return;
        CheckIfBecomePickable();
    }

    private void CheckIfBecomePickable()
    {
        var distance = (player.position - transform.position).magnitude;
        if (distance > distanceToBecomePickable)
        {
            isPickable = true;
            ColliderUtils.ChangeCollidersTrigger(gameObject, false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPickable && collision.gameObject.layer == KnownedLayers.Player)
        {
            inventoryBag.Add(itemPrefab);
            Destroy(gameObject);
        }
    }

    public void InitPrefab(GameObject itemPrefab)
    {
        this.itemPrefab = itemPrefab;
    }
}
