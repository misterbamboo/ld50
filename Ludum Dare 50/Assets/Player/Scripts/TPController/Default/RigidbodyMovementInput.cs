using UnityEngine;

namespace UPTK.TPController.Default
{
    public class RigidbodyMovementInput : MonoBehaviour, IMovementInput
    {
        [Header("MovementInput")]
        [SerializeField] private float inputForce = 500;
        private Rigidbody physicBody;

        private void Start()
        {
            physicBody = GetComponent<Rigidbody>();
        }

        public Vector3 GetRotatedMovement(float yAngle)
        {
            var raw = GetRawMovement();
            return Quaternion.AngleAxis(yAngle, Vector3.up) * raw;
        }

        public Vector3 GetRawMovement()
        {
            var timeSpeed = inputForce * Time.deltaTime;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            return new Vector3(horizontal * timeSpeed, 0, vertical * timeSpeed);
        }

        public void ApplyMovement(Vector3 movement)
        {
            if (physicBody != null)
            {
                physicBody.AddForce(movement);
            }
        }
    }
}
