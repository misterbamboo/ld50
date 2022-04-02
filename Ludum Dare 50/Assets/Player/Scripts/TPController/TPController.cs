using Assets.Player.Scripts.TPController.Default;
using Assets.Player.Scripts.TPController.GroundDetection;
using UnityEngine;
using UPTK.TPController.Default;

namespace UPTK.TPController
{
    public class TPController : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private ICameraInput cameraInput;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float Distance = 10;
        [SerializeField] private float AngleXMin = 0;
        [SerializeField] private float AngleXMax = 89;

        [Header("Movements")]
        [SerializeField] private IMovementInput movementInput;
        [SerializeField] private float jumpForce = 10;

        [Header("Jump")]
        [SerializeField] private IJumpInput jumpInput;
        [SerializeField] private GroundDetector _groundDetector;

        private IGroundDetector groundDetector => _groundDetector;

        private Rigidbody rbody;

        void Start()
        {
            if (cameraInput == null) cameraInput = GetOrCreateComponent<MouseCameraInput, ICameraInput>();
            if (cameraTransform == null) cameraTransform = Camera.main.transform;

            if (movementInput == null)
            {
                rbody = GetComponent<Rigidbody>();
                bool hasPhysicBody = rbody != null;
                if (hasPhysicBody)
                {
                    movementInput = GetOrCreateComponent<RigidbodyMovementInput, IMovementInput>();
                }
                else
                {
                    movementInput = GetOrCreateComponent<TransformMovementInput, IMovementInput>();
                }
            }

            if (jumpInput == null)
            {
                jumpInput = GetOrCreateComponent<KeyboardJumpInput, IJumpInput>();
            }
        }

        private TResult GetOrCreateComponent<TDefault, TResult>() where TDefault : Component, TResult
        {
            var cameraInput = GetComponent<TResult>();
            if (cameraInput == null)
            {
                cameraInput = gameObject.AddComponent<TDefault>();
            }

            return cameraInput;
        }

        void FixedUpdate()
        {
            PlaceCameraAroundPlayer();
            Move();
            Jump();
            ChangeCameraAngle();
        }

        private void PlaceCameraAroundPlayer()
        {
            var basePos = new Vector3(0, 0, -Distance);
            var clampXAngle = Mathf.Clamp(cameraInput.CurrentXAngle, AngleXMin, AngleXMax);
            var newOffset = Quaternion.Euler(clampXAngle, cameraInput.CurrentYAngle, 0) * basePos;
            cameraTransform.position = transform.position + newOffset;
        }

        private void Move()
        {
            var rotatedMovement = movementInput.GetRotatedMovement(cameraInput.CurrentYAngle);
            movementInput.ApplyMovement(rotatedMovement);
        }

        private void Jump()
        {
            if (jumpInput.JumpPressed())
            {
                if (groundDetector.IsGrounded && rbody != null)
                {
                    groundDetector.Unground();
                    rbody.AddForce(Vector3.up * jumpForce);
                }
            }
        }

        private void ChangeCameraAngle()
        {
            Vector3 targetDirection = transform.position - cameraTransform.position;
            cameraTransform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}