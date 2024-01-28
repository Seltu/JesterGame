using System;
using UnityEngine;

public class LavaJester_Interations : MonoBehaviour
{
    public event Action OnFalling;
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    private Rigidbody2D _jesterRb => _jesterLavaStats.GetJesterRigidBody();
    [SerializeField] private float _lavaImpulse;
    [SerializeField] private float invincibilityTime;
    private float _invincibilityTimer;
    private int _lavaHits;

    private void Update()
    {
        if (_invincibilityTimer > 0)
            _invincibilityTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Lava")
        {
            OnFalling?.Invoke();
            _jesterRb.velocity = Vector2.zero;
            _jesterRb.AddForce(new Vector2(0f, _lavaImpulse), ForceMode2D.Impulse);
            _lavaHits += 1;
            if (_invincibilityTimer <= 0 || _lavaHits >= 5){
                GameEventManager.OnTakeDamageTrigger();
                _invincibilityTimer = invincibilityTime;
            }

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
