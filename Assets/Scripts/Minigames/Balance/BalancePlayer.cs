using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlayer : MonoBehaviour
{
    public event Action<float> OnMovement;
    [SerializeField] private BalancePlayerStats playerStats;
    [SerializeField] private Rigidbody2D rb;
    void Update()
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

        if (Physics2D.Raycast(transform.position + Vector3.down, inputVector, 0.5f)) {
            rb.velocity = Vector2.zero;
            return;
        };

        OnMovement?.Invoke(inputVector.x * Time.deltaTime * playerStats.GetMovementSpeed());
        rb.velocity = inputVector * playerStats.GetMovementSpeed();
    }

    public BalancePlayerStats GetBalancePlayerStats()
    {
        return playerStats;
    }
}
