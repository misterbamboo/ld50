using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Inventory.Scripts
{
    public class InventoryBag : MonoBehaviour, IInventoryBag
    {
        [SerializeField] private List<GameObject> possibleItems;
        [SerializeField] private List<GameObject> inventory;
        [SerializeField] private GameObject currentItem;

        private void Start()
        {
            if (inventory == null)
                inventory = new List<GameObject>();

            FillInventoryWithRandomItems();
        }

        public event ItemSelectedHandler ItemSelected;

        public GameObject Peek() => currentItem;

        public void Add(GameObject item)
        {
            if (item == null)
                return;

            inventory?.Add(item);
        }

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
            {
                var random = UnityEngine.Random.Range(0, possibleItems.Count);
                Add(possibleItems[random]);
            }
        }

        private GameObject GetNextItem()
        {
            return inventory[0];
        }

        private void RaiseItemSelected(GameObject o) =>
            ItemSelected?.Invoke(this, new ItemSelectedEventArgs(o));
    }
}