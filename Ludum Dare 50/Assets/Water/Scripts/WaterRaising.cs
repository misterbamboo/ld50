using Assets.GameManagement;
using UnityEngine;

public class WaterRaising : MonoBehaviour
{
    public void FixedUpdate()
    {
        transform.position = new Vector3(
            x: transform.position.x,
            y: GameManager.Instance.FloodLevel.FloodHeight,
            z: transform.position.z
        );
    }
}
