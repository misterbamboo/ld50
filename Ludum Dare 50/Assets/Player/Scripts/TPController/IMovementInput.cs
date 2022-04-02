using UnityEngine;

namespace UPTK.TPController
{
    public interface IMovementInput
    {
        Vector3 GetRotatedMovement(float yAngle);
        Vector3 GetRawMovement();
        void ApplyMovement(Vector3 rotatedMovement);
    }
}
