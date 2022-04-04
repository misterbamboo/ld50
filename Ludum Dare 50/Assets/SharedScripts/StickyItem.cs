using Assets.GameManagement;
using Assets.SharedScripts;
using Assets.Utils;
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

            if (transform.GetComponent<PickableItem>() is PickableItem pickableItem)
            {
                Destroy(pickableItem);
            }

            LayerUtils.SetLayerRecursively(transform.gameObject, KnownedLayers.Tower);
            GameManager.Instance.ChangeTowerHeight(transform.position);
            EffectManager.Instance.SmokePoofAt(collision.contacts[0].point);
        }
    }
}
