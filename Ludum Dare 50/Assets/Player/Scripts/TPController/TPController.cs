using Assets.Player.Scripts.TPController.Default;
using Assets.Player.Scripts.TPController.GroundDetection;
using Assets.Player.Scripts.WaterDetection;
using UnityEngine;
using UPTK.TPController.Default;

namespace UPTK.TPController
{
    public interface ITPController
    {
        bool IsJumping { get; }

        bool IsMoving { get; }

        bool IsGrounded { get; }

        bool IsFalling { get; }

        bool IsSwiming { get; }
    }

    public class TPController : MonoBehaviour, ITPController
    {
        [Header("Camera")]
        [SerializeField] private ICameraInput cameraInput;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float Distance = 10;
        [SerializeField] private float AngleXMin = 1;
        [SerializeField] private float AngleXMax = 89;

        [Header("Movements")]
        [SerializeField] private IMovementInput movementInput;
        [SerializeField] private float jumpForce = 10;

        [Header("Jump")]
        [SerializeField] private IJumpInput jumpInput;
        [SerializeField] private GroundDetector _groundDetector;
        private IGroundDetector groundDetector => _groundDetector;

        [Header("Swim")]
        [SerializeField] private WaterDetection _waterDetector;
        private IWaterDetector waterDetector => _waterDetector;


        public bool IsJumping { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsGrounded => groundDetector.IsGrounded;
        public bool IsFalling { get; private set; }
        public bool IsSwiming => waterDetector.IsInWater;


        private Rigidbody rbody;
        private float lastY;

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
            FrameReset();

            PlaceCameraAroundPlayer();
            Move();
            Jump();
            ChangeCameraAngle();
            CheckFalling();
        }

        private void FrameReset()
        {
            IsJumping = false;
            IsMoving = false;
            // IsGrounded = false;
            IsFalling = false;
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

            IsMoving = rotatedMovement.magnitude > 0.01f;
        }

        private void Jump()
        {
            if (jumpInput.JumpPressed())
            {
                if (CanJump())
                {
                    groundDetector.Unground();

                    // Add force only when not already going up
                    if (rbody.velocity.y < 0.1f)
                    {
                        rbody.AddForce(Vector3.up * jumpForce);
                        IsJumping = true;
                    }
                }
            }
        }

        private bool CanJump()
        {
            return (IsSwiming || IsGrounded) && rbody != null;
        }

        private void ChangeCameraAngle()
        {
            Vector3 targetDirection = transform.position - cameraTransform.position;
            cameraTransform.rotation = Quaternion.LookRotation(targetDirection);
        }

        private void CheckFalling()
        {
            if (!groundDetector.IsGrounded)
            {
                var isFalling = transform.position.y - lastY < -0.01f;
                if (isFalling)
                {
                    IsFalling = true;
                }
            }

            lastY = transform.position.y;
        }
    }
}