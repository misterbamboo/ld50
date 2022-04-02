using Assets.SharedScripts;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCameraDisplay : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    private GameObject PeakNextItemPrefab()
    {
        return itemPrefab;
    }

    [SerializeField] Transform targetDisplayPosition;
    private GameObject currentDisplayedItem;
    private float currentDisplayedItemRotation;

    void Start()
    {
        NextItemChanged();
    }

    private void NextItemChanged()
    {
        currentDisplayedItemRotation = 0;
        var prefab = PeakNextItemPrefab();
        currentDisplayedItem = Instantiate(prefab, targetDisplayPosition.position, targetDisplayPosition.rotation);
        LayerUtils.SetLayerRecursively(currentDisplayedItem.gameObject, KnownedLayers.ItemCamera);

        if (currentDisplayedItem.GetComponent<Rigidbody>() is Rigidbody rb)
        {
            Destroy(rb);
        }
    }

    void Update()
    {
        if (currentDisplayedItem == null) return;

        currentDisplayedItemRotation += Time.deltaTime * 90f;

        var currentRotation = currentDisplayedItem.transform.rotation;
        var euler = currentDisplayedItem.transform.rotation.eulerAngles;
        currentDisplayedItem.transform.rotation = Quaternion.Euler(euler.x, currentDisplayedItemRotation, euler.z);
    }
}
