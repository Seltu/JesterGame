using System;
using UnityEngine;

public class LavaJester_Interations : MonoBehaviour
{
    public event Action OnFalling;
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    private Rigidbody2D _jesterRb => _jesterLavaStats.GetJesterRigidBody();
    private AudioSource _jesterAudio => _jesterLavaStats.GetJesterAudioSource();
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
            _jesterAudio.PlayOneShot(_jesterLavaStats.GetJesterScreamSFX());
            OnFalling?.Invoke();
            _jesterRb.velocity = Vector2.zero;
            _jesterRb.AddForce(new Vector2(0f, _lavaImpulse), ForceMode2D.Impulse);
            _lavaHits += 1;
            if (_invincibilityTimer <= 0 || _lavaHits >= 3){
                GameEventManager.OnTakeDamageTrigger();
                _invincibilityTimer = invincibilityTime;
                _lavaHits = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Finish_Line")
        {
            Debug.Log("AAA");
            GameEventManager.AddScoreTrigger(100);
        }
    }
}
