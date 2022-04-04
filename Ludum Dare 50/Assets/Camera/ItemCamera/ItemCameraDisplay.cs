using Assets.GameManagement;
using Assets.SharedScripts;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Camera.ItemCamera
{
    public class ItemCameraDisplay : MonoBehaviour
    {
        [SerializeField] Transform targetDisplayPosition;
        private GameObject currentDisplayedItem;
        private float currentDisplayedItemRotation;

        private Dictionary<string, GameObject> cacheDisplayedItems = new Dictionary<string, GameObject>();

        void Start()
        {
            GameManager.Instance.InventoryBag.NextItemChanged += InventoryBag_NextItemChanged;
            NextItemChanged();
        }

        private void InventoryBag_NextItemChanged(object sender, Inventory.Scripts.ItemSelectedEventArgs e)
        {
            NextItemChanged();
        }

        private void NextItemChanged()
        {
            ResetDisplayedItemRotation();
            RemoveCurrentCacheItems();
            DisplayNextItem();
        }

        private void RemoveCurrentCacheItems()
        {
            foreach (var cacheDisplayedItem in cacheDisplayedItems.Values)
            {
                cacheDisplayedItem.SetActive(false);
            }
        }

        private void DisplayNextItem()
        {
            var nextItemPrefab = GameManager.Instance.InventoryBag.Peek();
            if (nextItemPrefab != null)
            {
                currentDisplayedItem = GetOrCreateInstanceFromPrefab(nextItemPrefab);
                LayerUtils.SetLayerRecursively(currentDisplayedItem.gameObject, KnownedLayers.ItemCamera);
                currentDisplayedItem.SetActive(true);
            }
        }

        private GameObject GetOrCreateInstanceFromPrefab(GameObject prefab)
        {
            if (!cacheDisplayedItems.ContainsKey(prefab.name))
            {
                cacheDisplayedItems[prefab.name] = CreateNewInstance(prefab);
            }

            return cacheDisplayedItems[prefab.name];
        }

        private GameObject CreateNewInstance(GameObject nextItemPrefab)
        {
            var instance = Instantiate(nextItemPrefab, targetDisplayPosition.position, targetDisplayPosition.rotation);
            if (instance.GetComponent<Rigidbody>() is Rigidbody rb)
            {
                Destroy(rb);
            }
            return instance;
        }

        private void ResetDisplayedItemRotation()
        {
            currentDisplayedItemRotation = 0;
        }

        void Update()
        {
            if (currentDisplayedItem == null) return;
            currentDisplayedItemRotation += Time.deltaTime * 90f;

            var euler = currentDisplayedItem.transform.rotation.eulerAngles;
            currentDisplayedItem.transform.rotation = Quaternion.Euler(euler.x, currentDisplayedItemRotation, euler.z);
        }
    }
}