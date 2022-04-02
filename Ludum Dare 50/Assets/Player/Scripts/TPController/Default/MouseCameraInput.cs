using UnityEngine;

namespace UPTK.TPController.Default
{
    public class MouseCameraInput : MonoBehaviour, ICameraInput
    {
        [SerializeField] private bool invertRotationXAxis = false;
        [SerializeField] private bool invertRotationYAxis = true;
        [SerializeField] private float mouseRotationSpeed = 0.5f;
        [SerializeField] private float startingXAngle = 15f;

        public float CurrentYAngle => currentYAngle;
        public float CurrentXAngle => currentXAngle;

        private float initialMouseXDragStart;
        private float initialDragYAngle;
        private float currentYAngle;

        private float initialMouseYDragStart;
        private float initialDragXAngle;
        private float currentXAngle;

        private void Start()
        {
            currentXAngle = startingXAngle;
        }

        private void Update()
        {
            TrackMouseDragStart();
            TrackMouseDragging();
        }

        private void TrackMouseDragStart()
        {
            if (MouseClick())
            {
                MouseXDragStart();
                MouseYDragStart();
            }
        }

        private static bool MouseClick()
        {
            return Input.GetMouseButtonDown(1);
        }

        private void MouseXDragStart()
        {
            initialMouseXDragStart = Input.mousePosition.x;
            initialDragYAngle = currentYAngle;
        }

        private void MouseYDragStart()
        {
            initialMouseYDragStart = Input.mousePosition.y;
            initialDragXAngle = currentXAngle;
        }

        private void TrackMouseDragging()
        {
            if (MouseIsDragging())
            {
                MouseDragging();
            }
        }

        private static bool MouseIsDragging()
        {
            return Input.GetMouseButton(1);
        }

        private void MouseDragging()
        {
            FollowMouseDragX();
            FollowMouseDragY();
        }

        private void FollowMouseDragX()
        {
            var currentMouseXDiff = invertRotationYAxis ?
                    (Input.mousePosition.x - initialMouseXDragStart) * mouseRotationSpeed :
                    (initialMouseXDragStart - Input.mousePosition.x) * mouseRotationSpeed;

            currentYAngle = initialDragYAngle + currentMouseXDiff;
        }

        private void FollowMouseDragY()
        {
            var currentMouseYDiff = invertRotationXAxis ?
                    (Input.mousePosition.y - initialMouseYDragStart) * mouseRotationSpeed :
                    (initialMouseYDragStart - Input.mousePosition.y) * mouseRotationSpeed;

            currentXAngle = initialDragXAngle + currentMouseYDiff;
        }
    }
}
