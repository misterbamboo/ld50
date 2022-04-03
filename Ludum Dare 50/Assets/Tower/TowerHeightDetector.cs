using Assets.GameManagement;
using Assets.SharedScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Tower
{
    public interface ITowerHeightDetector
    {
        float TowerHeight { get; }
        void RecalculateHeight(Vector3 newTowerItemPosition);
    }

    public class TowerHeightDetector : MonoBehaviour, ITowerHeightDetector
    {
        [SerializeField] float initialHeight = 2;
        public float TowerHeight => currentTowerHeight;

        private float currentTowerHeight;

        private float minCheckTowerHeight;
        private int touchTowerCount;
        private float bestHeight;

        public void RecalculateHeight(Vector3 newTowerItemPosition)
        {
            if (newTowerItemPosition.y > minCheckTowerHeight)
            {
                minCheckTowerHeight = newTowerItemPosition.y;
            }
        }

        private void Start()
        {
            currentTowerHeight = initialHeight;
            minCheckTowerHeight = currentTowerHeight;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == KnownedLayers.Tower)
            {
                touchTowerCount += 1;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == KnownedLayers.Tower)
            {
                touchTowerCount -= 1;
            }
        }

        private void FixedUpdate()
        {
            if (ShouldDrop())
            {
                DropOneStep();
            }
            else
            {
                RaiseMore();
            }

            if (bestHeight > currentTowerHeight)
            {
                currentTowerHeight = bestHeight;
                if (currentTowerHeight > minCheckTowerHeight)
                {
                    minCheckTowerHeight = currentTowerHeight;
                }
            }
        }

        private bool ShouldDrop()
        {
            if (currentTowerHeight > minCheckTowerHeight)
            {
                return true;
            }

            if (touchTowerCount <= 0)
            {
                return transform.position.y > currentTowerHeight;
            }

            return false;
        }

        private void RaiseMore()
        {
            if (currentTowerHeight < minCheckTowerHeight)
            {
                RaiseAStep();
            }
            else
            {
                RaiseALittle();
            }
        }

        private void RaiseALittle()
        {
            bestHeight = transform.position.y;
            transform.position += new Vector3(0, 0.1f, 0);
        }

        private void RaiseAStep()
        {
            bestHeight = transform.position.y;
            transform.position += new Vector3(0, 1, 0);
        }

        private void DropOneStep()
        {
            var currentStep = (int)Math.Floor(transform.position.y);
            var pos = transform.position;
            pos.y = currentStep - 1;
            transform.position = pos;
        }
    }
}
