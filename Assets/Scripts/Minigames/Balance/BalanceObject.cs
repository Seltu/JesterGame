using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float fallSpeed;
    private bool hasHit;

    private void Update()
    {
        if (!hasHit)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
        else
            GameEventManager.AddScoreTrigger(0.5f*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasHit)
        {
            hasHit = true;
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("OffscreenLimit"))
        GameEventManager.AddScoreTrigger(-30);
        Destroy(gameObject);
    }

}
