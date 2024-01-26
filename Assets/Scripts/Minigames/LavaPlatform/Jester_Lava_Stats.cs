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

    // Layer Masks
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _wallLayerMask;

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

    public LayerMask GetGroundLayerMask()
    {
        return _groundLayerMask;
    }

    public LayerMask GetWallLayerMask()
    {
        return _wallLayerMask;
    }
}
