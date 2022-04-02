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

        private void OnTriggerEnter(Collider other)
        {
            IsGrounded = true;
        }

        private void OnTriggerExit(Collider other)
        {
            IsGrounded = false;
        }

        public void Unground()
        {
            IsGrounded = false;
        }
    }
}
