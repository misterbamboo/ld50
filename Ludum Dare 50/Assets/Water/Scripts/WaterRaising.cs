using Assets.GameManagement;
using UnityEngine;

public class WaterRaising : MonoBehaviour
{
    public void FixedUpdate()
    {
        var floodHeight = GameManager.Instance.FloodLevel.FloodHeight;
        transform.position = new Vector3(
            x: transform.position.x,
            y: float.IsInfinity(floodHeight) ? float.MaxValue : floodHeight,
            z: transform.position.z
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        EffectManager.Instance.SplashAt(other.transform.position);
    }
}
