using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectWhenFinishedScript : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
    }

    private void FixedUpdate()
    {
        if (ps != null && !ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
