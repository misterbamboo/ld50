using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPTK.TPController;

public class FranbecController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isFalling;
    [SerializeField] bool isMoving;
    [SerializeField] bool isSwimming;
    [SerializeField] bool isThrowing;


    [SerializeField] PlaceNextItem placeNextItemController;

    [SerializeField] TPController TPControllerReference;
    private ITPController TPController => TPControllerReference;

    private void FixedUpdate()
    {
        isJumping = TPController.IsJumping;
        isMoving = TPController.IsMoving;
        isGrounded = TPController.IsGrounded;
        isFalling = TPController.IsFalling;
        isSwimming = TPController.IsSwiming;

        isThrowing = placeNextItemController.IsThrowing;
        placeNextItemController.ResetIsThrowing();

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsSwimming", isSwimming);
        animator.SetBool("IsThrowing", isThrowing);
    }
}
