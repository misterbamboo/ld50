using UnityEngine;

public class WaterRaising : MonoBehaviour
{
    [SerializeField] private float raisingSpeed = 1;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += Vector3.up * Time.fixedDeltaTime * raisingSpeed;
    }
}
