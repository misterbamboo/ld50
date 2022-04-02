using UnityEngine;

namespace UPTK.TPController.Default
{
    public class TransformMovementInput : MonoBehaviour, IMovementInput
    {
        [Header("Controller")]
        [SerializeField] private float inputSpeed = 5;

        public Vector3 GetRotatedMovement(float yAngle)
        {
            var raw = GetRawMovement();
            return Quaternion.AngleAxis(yAngle, Vector3.up) * raw;
        }

        public Vector3 GetRawMovement()
        {
            var timeSpeed = inputSpeed * Time.deltaTime;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            return new Vector3(horizontal * timeSpeed, 0, vertical * timeSpeed);
        }

        public void ApplyMovement(Vector3 movement)
        {
            transform.position += movement;
        }
    }
}
