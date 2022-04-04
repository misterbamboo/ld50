using Assets.GameManagement;
using Assets.Inventory.Scripts;
using Assets.SharedScripts;
using Assets.Utils;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] float distanceToBecomePickable = 2;
    [SerializeField] bool isPickable = false;
    private Transform player;
    private IInventoryBag nventoryBag;
    private GameObject itemPrefab;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        nventoryBag = GameManager.Instance.InventoryBag;
    }

    private void FixedUpdate()
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
            nventoryBag.Add(itemPrefab);
            Destroy(gameObject);
        }
    }

    public void InitPrefab(GameObject itemPrefab)
    {
        this.itemPrefab = itemPrefab;
    }
}
