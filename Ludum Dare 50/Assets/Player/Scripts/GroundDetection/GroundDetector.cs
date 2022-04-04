using Assets.SharedScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player.Scripts.TPController.GroundDetection
{
    public interface IGroundDetector
    {
        bool IsGrounded { get; }
        void Unground();
    }

    public class GroundDetector : MonoBehaviour, IGroundDetector
    {
        public bool IsGrounded { get; private set; }

        private List<GameObject> groundedItems = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (KnownedLayers.IsGroundable(other.gameObject.layer))
            {
                RemoveDisposed();
                groundedItems.Add(other.gameObject);
                UpdateGrounded();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (KnownedLayers.IsGroundable(other.gameObject.layer))
            {
                RemoveDisposed();
                if (groundedItems.Contains(other.gameObject))
                {
                    groundedItems.Remove(other.gameObject);
                }
                UpdateGrounded();
            }
        }

        private void RemoveDisposed()
        {
            for (int i = 0; i < groundedItems.Count; i++)
            {
                var go = groundedItems[i];
                // Note: In unity, GameObject override "== null".
                // When object destroyed, the C# reference exists but the "== null" return true
                if (go == null)
                {
                    groundedItems.Remove(go);
                }
            }
        }

        private void UpdateGrounded()
        {
            IsGrounded = groundedItems.Count > 0;
        }

        public void Unground()
        {
            IsGrounded = false;
        }
    }
}
