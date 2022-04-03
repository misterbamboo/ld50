using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FranbecController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isFalling;
    [SerializeField] bool isMoving;

    void Update()
    {
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsMoving", isMoving);
    }
}
