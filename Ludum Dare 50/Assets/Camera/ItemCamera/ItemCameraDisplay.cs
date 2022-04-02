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

    void Start()
    {
        NextItemChanged();
    }

    private void NextItemChanged()
    {
        var prefab = PeakNextItemPrefab();
        currentDisplayedItem = Instantiate(prefab, targetDisplayPosition.position, Quaternion.identity);
        LayerUtils.SetLayerRecursively(currentDisplayedItem.gameObject, KnownedLayers.ItemCamera);

        if (currentDisplayedItem.GetComponent<Rigidbody>() is Rigidbody rb)
        {
            Destroy(rb);
        }
    }

    void Update()
    {
        if (currentDisplayedItem != null) return;

        var currentRotation = currentDisplayedItem.transform.rotation;
        currentRotation.y += Time.deltaTime * 90f;
        currentDisplayedItem.transform.rotation = currentRotation;
    }
}
