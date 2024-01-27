using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester_Lava_Visuals : MonoBehaviour
{
    [SerializeField] Jester_LavaMovement jesterMovement;
    [SerializeField] LavaJester_Interations jesterInteractions;
    [SerializeField] Animator jesterAnimator;
    private void Start()
    {
        jesterMovement.OnMoving += OnMoving;
        jesterMovement.OnSliding += OnSliding;
        jesterMovement.OnGrounded += OnGrounded;
        jesterMovement.OnJump += OnJump;
        jesterInteractions.OnFalling += OnFalling;
    }

    private void OnJump()
    {
        jesterAnimator.SetBool("isJumping", true);
    }

    private void OnGrounded()
    {
        jesterAnimator.SetBool("isFalling", false);
        jesterAnimator.SetBool("isJumping", false);
    }

    private void OnMoving(bool obj)
    {
        jesterAnimator.SetBool("isRunning", obj);
    }

    private void OnSliding(bool obj)
    {
        jesterAnimator.SetBool("isSliding", obj);
    }

    private void OnFalling()
    {
        jesterAnimator.SetBool("isFalling", true);
    }
}
