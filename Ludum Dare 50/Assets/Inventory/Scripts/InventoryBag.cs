using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBag : MonoBehaviour, IInventoryBag
{
    [SerializeField] private List<GameObject> possibleItems;
    [SerializeField] private List<GameObject> inventory;
    [SerializeField] private GameObject currentItem;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = new List<GameObject>();
            FillInventoryWithRandomItems();
        }
    }

    public event ItemSelectedHandler ItemSelected;

    public GameObject Peek() => currentItem;

    public GameObject Use()
    {
        var useItem = currentItem;
        inventory.RemoveAt(0);
        if (inventory.Count < 5)
            FillInventoryWithRandomItems();

        currentItem = GetNextItem();
        RaiseItemSelected(currentItem);

        return useItem;
    }

    private void FillInventoryWithRandomItems()
    {
        for (int i = 0; i < 10; i++)
            inventory.Add(possibleItems[UnityEngine.Random.Range(0, possibleItems.Count)]);
    }

    private GameObject GetNextItem()
    {
        throw new NotImplementedException();
    }

    private void RaiseItemSelected(GameObject o) =>
        ItemSelected?.Invoke(this, new ItemSelectedEventArgs(o));
}
