using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlayerStats : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float tiltScale;

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetTiltScale()
    {
        return tiltScale;
    }

}
