using Assets.SharedScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Player.Scripts.WaterDetection
{
    public interface IWaterDetector
    {
        bool IsInWater { get; }
    }

    public class WaterDetection : MonoBehaviour, IWaterDetector
    {
        public bool IsInWater { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == KnownedLayers.Water)
            {
                IsInWater = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == KnownedLayers.Water)
            {
                IsInWater = false;
            }
        }
    }
}
