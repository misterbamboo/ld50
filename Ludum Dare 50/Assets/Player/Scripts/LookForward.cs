using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UPTK.TPController;

namespace Assets.Player.Scripts
{
    internal class LookForward : MonoBehaviour
    {
        private Vector3 lastFlatPos;
        private bool isDestroyed;

        private void Start()
        {
            StartCoroutine(KeepLastPositionCoroutine());
        }

        private IEnumerator KeepLastPositionCoroutine()
        {
            while (!isDestroyed)
            {
                lastFlatPos = GetFlatPosition();
                yield return new WaitForSeconds(0.05f);
            }
        }

        private Vector3 GetFlatPosition()
        {
            return new Vector3(transform.position.x, 0, transform.position.z);
        }

        private void Update()
        {
            var direction = GetFlatPosition() - lastFlatPos;
            if (CanRotate(direction))
            {
                var rotation = Quaternion.LookRotation(direction);
                var wantedRotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, 0.3f);
            }
        }

        private bool CanRotate(Vector3 direction)
        {
            return direction.magnitude > 0.01f && Input.anyKey;
        }

        private void OnDestroy()
        {
            isDestroyed = true;
            StopAllCoroutines();
        }
    }
}
