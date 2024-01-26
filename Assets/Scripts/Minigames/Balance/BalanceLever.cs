using System;
using UnityEngine;

public class BalanceLever : MonoBehaviour
{
    [SerializeField] private BalancePlayer player;
    [SerializeField] private Rigidbody2D rb;
    private ObjectSpawner _objectSpawner;

    private float TiltScale => player.GetBalancePlayerStats().GetTiltScale();

    private void Start()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        player.OnMovement += AddInclination;
    }

    private void AddInclination(float movement)
    {
        rb.angularVelocity += movement * TiltScale;
    }

}
