using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    [SerializeField] float radius = 30;
    [SerializeField] float speed = 1;

    float totalTime;

    private void Start()
    {
        totalTime = UnityEngine.Random.Range(0, 5000f);
    }

    void Update()
    {
        totalTime += Time.deltaTime * speed;
        float z = Mathf.Sin(totalTime) * radius;
        float x = Mathf.Cos(totalTime) * radius;
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
