using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    private Rigidbody rb;
    public Rigidbody RigidBody => rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
