using Assets.GameManagement;
using UnityEngine;

public class WaterRaising : MonoBehaviour
{
    [SerializeField] private float raisingSpeed = 1;

    void FixedUpdate()
    {
        var pos = transform.position;
        pos.y = GameManager.Instance.FloodLevel.FloodHeight;
        transform.position = pos;
    }
}
