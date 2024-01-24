using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceLever : MonoBehaviour
{
    [SerializeField] private BalancePlayer player;
    private BalancePlayerStats balanceStats;
    private float _inclination;

    private float MaxAngle => player.GetBalancePlayerStats().GetMaxAngle();
    private float TiltScale => player.GetBalancePlayerStats().GetTiltScale();
    private float GravityScale => player.GetBalancePlayerStats().GetGravityScale();

    private void Start()
    {
        player.OnMovement += AddInclination;
        GameEventManager.balanceObjectWeight += AddInclination;
    }

    private void AddInclination(float movement)
    {
        _inclination += movement * TiltScale;
        _inclination = Mathf.Clamp(_inclination, -MaxAngle, MaxAngle);
    }

    private void Update()
    {
        if (_inclination > -MaxAngle && _inclination < MaxAngle)
        {
            _inclination += _inclination * GravityScale * Time.deltaTime;
            _inclination = Mathf.Clamp(_inclination, -MaxAngle, MaxAngle);
            transform.rotation = Quaternion.AngleAxis(_inclination, Vector3.forward);
        }
    }
}
