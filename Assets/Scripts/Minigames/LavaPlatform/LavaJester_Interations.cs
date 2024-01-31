using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LavaJester_Interations : MonoBehaviour
{
    public event Action OnFalling;
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    private Rigidbody2D _jesterRb => _jesterLavaStats.GetJesterRigidBody();
    private AudioSource _jesterAudio => _jesterLavaStats.GetJesterAudioSource();
    [SerializeField] private float _lavaImpulse;
    [SerializeField] private float invincibilityTime;
    private float _invincibilityTimer;
    private float _lavaStayTimer;

    private void Update()
    {
        if (_invincibilityTimer > 0)
            _invincibilityTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Lava")
        {
            _jesterAudio.volume = 1f;
            _jesterAudio.PlayOneShot(_jesterLavaStats.GetJesterScreamSFX());
            OnFalling?.Invoke();
            _jesterRb.velocity = Vector2.zero;
            _jesterRb.AddForce(new Vector2(0f, _lavaImpulse), ForceMode2D.Impulse);
            _lavaStayTimer = 0.1f;
            if (_invincibilityTimer <= 0){
                GameEventManager.OnTakeDamageTrigger();
                _invincibilityTimer = invincibilityTime;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lava")
        {
            if (_lavaStayTimer > 0)
                _lavaStayTimer -= Time.deltaTime;
            else
            {
                // >:) skill issue lmao
                GameEventManager.OnTakeDamageTrigger();
                _lavaStayTimer = 0.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Finish_Line")
        {
            StartCoroutine(Win());
        }
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(2f);
        GameEventManager.EndSceneTrigger();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndCutScene");
    }
}
