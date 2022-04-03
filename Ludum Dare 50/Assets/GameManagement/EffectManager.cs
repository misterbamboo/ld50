using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagement
{
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] GameObject smokePoofPrefab;
        [SerializeField] GameObject splashPrefab;

        public void SmokePoofAt(Vector3 point)
        {
            Instantiate(smokePoofPrefab, point, Quaternion.identity);
        }

        public void SplashAt(Vector3 point)
        {
            Instantiate(splashPrefab, point, Quaternion.identity);
        }
    }
}
