using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceLever : MonoBehaviour
{
    [SerializeField] private BalancePlayer player;
    [SerializeField] private Rigidbody2D rb;
    private ObjectSpawner _objectSpawner;
    private float _inclinationToAdd;

    private float Inclination => transform.rotation.z;
    private float MaxAngle => player.GetBalancePlayerStats().GetMaxAngle();
    private float TiltScale => player.GetBalancePlayerStats().GetTiltScale();

    private void Start()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        player.OnMovement += AddInclination;
    }

    private void AddInclination(float movement)
    {
        _inclinationToAdd += movement * TiltScale * (1+Mathf.Log(_objectSpawner.GetObjectCount()));
    }


    private void Update()
    {
        if (Inclination >= -MaxAngle && Inclination <= MaxAngle)
        {
            rb.AddTorque(_inclinationToAdd);
            _inclinationToAdd = 0;
        }
    }
}
