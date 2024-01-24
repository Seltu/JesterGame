using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceObject : MonoBehaviour
{
    [SerializeField] private float weight;
    private void OnTriggerStay2D(Collider2D collider)
    {
        var weightForce = -weight * (transform.position.x - collider.bounds.center.x) / collider.bounds.extents.x;
        GameEventManager.BalanceObjectWeightTrigger(weightForce);
    }
}
