using Assets.SharedScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<StickyItem>() != null)
        {
            if (transform.GetComponent<Rigidbody>() is Rigidbody rb)
            {
                rb.isKinematic = true;
            }
        }

        SetLayerRecursively(transform.gameObject, KnownedLayers.Ground);
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
