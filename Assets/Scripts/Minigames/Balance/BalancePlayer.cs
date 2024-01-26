using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlayer : MonoBehaviour
{
    public event Action<float> OnMovement;
    [SerializeField] private BalancePlayerStats playerStats;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private HingeJoint2D leverHingeJoint;
    [SerializeField] private MinigameManager minigameManager;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        if (minigameManager != null)
            minigameManager.OnGameWin += FlipOver;
    }

    private void FlipOver()
    {
        leverHingeJoint.enabled = false;
        playerAnimator.SetTrigger("Fall");
        rb.velocity = Vector2.zero;
        this.enabled = false;
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }

        inputVector = inputVector.normalized;
    
        OnMovement?.Invoke(inputVector.x * Time.deltaTime * playerStats.GetMovementSpeed());
        rb.velocity = inputVector * playerStats.GetMovementSpeed();
    }

    public BalancePlayerStats GetBalancePlayerStats()
    {
        return playerStats;
    }
}
