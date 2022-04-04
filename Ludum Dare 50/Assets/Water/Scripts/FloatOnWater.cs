using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloatOnWater : MonoBehaviour
{

    [SerializeField] float slowDownFactor = 0.985f;

    [SerializeField] float sinWave = 0.2f;
    [SerializeField] float sinWaveSpeed = 2f;

    [SerializeField] float topOffset = 0.3f;
    [SerializeField] Transform topPos;
    private List<GameObject> inWaterItems = new List<GameObject>();

    private float time;

    private void Update()
    {
        time += Time.deltaTime * sinWaveSpeed;
        if (time > Mathf.PI * 2)
        {
            time -= Mathf.PI * 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            inWaterItems.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (inWaterItems.Contains(other.gameObject))
        {
            inWaterItems.Remove(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        CleanDestroyedItems();

        foreach (var inWaterItem in inWaterItems)
        {
            if (inWaterItem == null)
            {
                continue;
            }

            var rb = inWaterItem.GetComponent<Rigidbody>();

            var diff = topPos.position.y - inWaterItem.transform.position.y;
            float offsetDiff = AdjustOffset(diff);
            if (offsetDiff > 0)
            {
                var xVelocity = rb.velocity.x * slowDownFactor;
                var yVelocity = rb.velocity.y * slowDownFactor;
                rb.velocity = new Vector3(xVelocity, offsetDiff, yVelocity);
                rb.angularVelocity = rb.angularVelocity * slowDownFactor;
            }
        }
    }

    private void CleanDestroyedItems()
    {
        for (int i = 0; i < inWaterItems.Count; i++)
        {
            if (inWaterItems[i] == null)
            {
                inWaterItems.RemoveAt(i);
            }
        }
    }

    private float AdjustOffset(float diff)
    {
        var variableOffset = Mathf.Sin(time) * sinWave;
        return diff + topOffset + variableOffset;
    }
}
