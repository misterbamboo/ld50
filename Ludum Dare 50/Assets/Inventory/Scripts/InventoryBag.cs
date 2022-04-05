using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Inventory.Scripts
{
    public class InventoryBag : MonoBehaviour, IInventoryBag
    {
        [SerializeField] private List<GameObject> possibleItems;
        [SerializeField] private List<GameObject> inventory;
        [SerializeField] private GameObject currentItem;
        [SerializeField] private int inventoryStartingCount = 5;

        private void Start()
        {
            if (inventory == null)
            {
                inventory = new List<GameObject>();
            }
            FillInventoryWithRandomItems();
        }

        public event ItemSelectedHandler NextItemChanged;

        public GameObject Peek() => inventory.FirstOrDefault();

        public void Add(GameObject item)
        {
            if (item == null)
                return;

            inventory?.Add(item);

            if (inventory.Count > 0)
            {
                RaiseNextItemChanged(inventory[0]);
            }
        }

        public GameObject PickNext()
        {
            if (inventory.Count <= 0)
            {
                return null;
            }

            var useItem = inventory.FirstOrDefault();
            inventory.RemoveAt(0);

            RaiseNextItemChanged(Peek());

            return useItem;
        }

        private void FillInventoryWithRandomItems()
        {
            if (inventoryStartingCount <= 0)
            {
                return;
            }

            for (int i = 0; i < inventoryStartingCount; i++)
            {
                var random = UnityEngine.Random.Range(0, possibleItems.Count);
                Add(possibleItems[random]);
            }
        }

        private void RaiseNextItemChanged(GameObject o) =>
            NextItemChanged?.Invoke(this, new ItemSelectedEventArgs(o));
    }
}