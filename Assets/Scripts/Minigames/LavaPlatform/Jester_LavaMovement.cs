using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jester_LavaMovement : MonoBehaviour
{
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    [SerializeField] private SpriteRenderer _jesterSpriteRenderer;
    [SerializeField] private LavaJester_Interations jesterInteractions;
    [SerializeField] private float _wallDrag;
    private Rigidbody2D _jesterRb => _jesterLavaStats.GetJesterRigidBody();
    private Collider2D _jesterCollider => _jesterLavaStats.GetJesterCollider();
    private LayerMask _groundLayerMask => _jesterLavaStats.GetGroundLayerMask();
    private LayerMask _wallLayerMask => _jesterLavaStats.GetWallLayerMask();
    
    private float _midairTimer;
    private bool _isSliding = false;
    private bool _grounded;

    public event Action<bool> OnMoving;
    public event Action<bool> OnSliding;
    public event Action OnJump;
    public event Action OnGrounded;

    private void Start()
    {
        jesterInteractions.OnFalling += HandleFalling;
    }

    private void HandleFalling()
    {
        _grounded = false;
        int wallHit = CheckWall();
        if (wallHit > 0)
        {
            _isSliding = true;
            OnSliding?.Invoke(true);
            _jesterRb.drag = _wallDrag;
        }
        else if (wallHit < 0)
        {
            _isSliding = true;
            _jesterRb.drag = _wallDrag;
            OnSliding?.Invoke(true);
        }
    }

    private void Update()
    {
        //Debug.DrawRay(_jesterCollider.bounds.center + new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        //Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        //Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, _jesterCollider.bounds.extents.y), Vector2.right * (_jesterCollider.bounds.extents.x));

        if(!_isSliding&&_midairTimer<=0)
            PlayerMovement();


        if (Input.GetKeyDown(KeyCode.Space)&&!_grounded&&_isSliding)
        {
            PlayerWallJump();
            _jesterRb.drag = 0;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (_grounded && !_isSliding)
            {
                PlayerJump();
            }
        }

        if (_midairTimer > 0)
            _midairTimer -= Time.deltaTime;
        else
            _midairTimer = 0;
    }

    private void PlayerMovement()
    {
        float inputVector = Input.GetAxisRaw("Horizontal");

        if(inputVector < 0)
            _jesterSpriteRenderer.flipX = true;
        else if (inputVector > 0)
            _jesterSpriteRenderer.flipX = false;

        OnMoving?.Invoke(inputVector!=0);

        _jesterRb.velocity = new Vector2(inputVector * _jesterLavaStats.GetLavaJesterSpeed(), _jesterRb.velocity.y);
    }
    
    private void PlayerJump()
    {
        OnJump?.Invoke();
        _jesterRb.AddForce(new Vector2(0, _jesterLavaStats.GetLavaJesterJump()));
        _grounded = false;

        int wallHit = CheckWall();
        if (wallHit > 0)
        {
            _isSliding = true;
            OnSliding?.Invoke(true);
            _jesterRb.drag = _wallDrag;
        }
        else if (wallHit < 0)
        {
            _isSliding = true;
            _jesterRb.drag = _wallDrag;
            OnSliding?.Invoke(true);
        }
    }

    private void PlayerWallJump()
    {   
        float horizontalForce = CheckWall();

        if (horizontalForce < 0)
            _jesterSpriteRenderer.flipX = true;
        else if (horizontalForce > 0)
            _jesterSpriteRenderer.flipX = false;

        float horiontalJump = _jesterLavaStats.GetLavaJesterJump() * horizontalForce / 2;
        float verticalJump = _jesterLavaStats.GetLavaJesterJump();

        _jesterRb.drag = 0;
        _isSliding = false;
        _grounded = false;
        _midairTimer = 0.4f;
        OnSliding?.Invoke(false);
        _jesterRb.AddForce(new Vector2(horiontalJump, verticalJump));
    }

    private int CheckWall()
    {
        bool leftWallHit = Physics2D.Raycast(_jesterCollider.bounds.center, Vector2.left, 0.5f, _wallLayerMask);
        bool rightWallHit = Physics2D.Raycast(_jesterCollider.bounds.center, Vector2.right, 0.5f, _wallLayerMask);

        if (leftWallHit)
            return 1;
        else if (rightWallHit)
            return -1;
        return 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {

            if (!_grounded && CheckWall() != 0)
            {
                _isSliding = true;
                OnSliding?.Invoke(true);
            }
            _jesterRb.drag = _wallDrag;
        }


        RaycastHit2D raycastHit = Physics2D.BoxCast(_jesterCollider.bounds.center, new Vector2(_jesterCollider.bounds.size.x, 0.1f), 0f, Vector2.down, 1f, _groundLayerMask);

        if (raycastHit)
        {
            _isSliding = false;
            _jesterRb.drag = 0;
        }

        if(raycastHit.collider != null) {
            OnGrounded?.Invoke();
        }
        _grounded = raycastHit.collider != null;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            if (CheckWall() == 0)
            {
                _isSliding = false;
                _jesterRb.drag = 0;
                OnSliding?.Invoke(false);
            }
        }
    }
} 
