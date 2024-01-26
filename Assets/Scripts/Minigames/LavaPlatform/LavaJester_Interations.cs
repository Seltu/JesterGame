using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class LavaJester_Interations : MonoBehaviour
{
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    private Rigidbody2D _jesterRb => _jesterLavaStats.GetJesterRigidBody();
    [SerializeField] private float _lavaImpulse;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Lava")
        {
            _jesterRb.velocity = Vector2.zero;
            _jesterRb.AddForce(new Vector2(0f, _lavaImpulse), ForceMode2D.Impulse);
            GameEventManager.OnTakeDamageTrigger();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Finish_Line")
        {
            GameEventManager.AddScoreTrigger(100);
        }
    }
}
