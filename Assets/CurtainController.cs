using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    [SerializeField] private bool startOpen;
    [SerializeField] private Animator curtainAnimator;
    private void OnEnable()
    {
        GameEventManager.endScene += CloseCurtains;
    }

    private void OnDisable()
    {
        GameEventManager.endScene -= CloseCurtains;
    }

    private void Start()
    {
        if (startOpen)
        {
            curtainAnimator.SetBool("Close", false);
            curtainAnimator.Play("CurtainsOpened");
        }
    }

    private void CloseCurtains()
    {
        curtainAnimator.SetBool("Close", true);
    }
}
