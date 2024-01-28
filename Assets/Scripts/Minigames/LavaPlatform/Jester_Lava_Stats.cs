using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester_Lava_Stats : MonoBehaviour
{
    // General Stats
    [SerializeField] private float _jesterSpeed;
    [SerializeField] private float _jumpForce;

    // Components
    [SerializeField] private Rigidbody2D _jesterRb;
    [SerializeField] private Collider2D _jesterCollider;
    [SerializeField] private AudioSource _jesterAudioSource;

    // Layer Masks
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _wallLayerMask;

    // Audios
    [SerializeField] private AudioClip _runningSFX;
    [SerializeField] private AudioClip _screamSFX;
    [SerializeField] private AudioClip[] _jumpSFX;

    public float GetLavaJesterSpeed()
    {
        return _jesterSpeed;
    }

    public float GetLavaJesterJump()
    {
        return _jumpForce;
    }

    public Rigidbody2D GetJesterRigidBody()
    {
        return _jesterRb;
    }

    public Collider2D GetJesterCollider()
    {
        return _jesterCollider;
    }

    public AudioSource GetJesterAudioSource()
    {
        return _jesterAudioSource;
    }

    public LayerMask GetGroundLayerMask()
    {
        return _groundLayerMask;
    }

    public LayerMask GetWallLayerMask()
    {
        return _wallLayerMask;
    }

    public AudioClip GetJesterScreamSFX()
    {
        return _screamSFX;
    }

    public AudioClip GetJesterJumpSFX()
    {
        int randomizer = Random.Range(0, 3);
        Debug.Log(randomizer);
        return _jumpSFX[randomizer];
    }

    public AudioClip GetJesterRunningSFX()
    {
        return _runningSFX;
    }
}
