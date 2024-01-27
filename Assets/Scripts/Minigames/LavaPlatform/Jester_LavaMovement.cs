using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jester_LavaMovement : MonoBehaviour
{
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    [SerializeField] private SpriteRenderer _jesterSpriteRenderer;
    private Rigidbody2D _jesterRb => _jesterLavaStats.GetJesterRigidBody();
    private Collider2D _jesterCollider => _jesterLavaStats.GetJesterCollider();
    private LayerMask _groundLayerMask => _jesterLavaStats.GetGroundLayerMask();
    private LayerMask _wallLayerMask => _jesterLavaStats.GetWallLayerMask();
    
    [SerializeField] private float _wallDrag;
    private float _midairTimer;
    private bool _isSliding = false;
    private bool _grounded;

    void Update()
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

        _jesterRb.velocity = new Vector2(inputVector * _jesterLavaStats.GetLavaJesterSpeed(), _jesterRb.velocity.y);
    }
    
    private void PlayerJump()
    {
        _jesterRb.AddForce(new Vector2(0, _jesterLavaStats.GetLavaJesterJump()));
        _grounded = false;

        RaycastHit2D leftWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.left, .01f, _wallLayerMask);
        RaycastHit2D rightWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.right, .01f, _wallLayerMask);

        if (leftWallHit)
        {
            _isSliding = true;
            _jesterRb.drag = _wallDrag;
        }
        else if (rightWallHit)
        {
            _isSliding = true;
            _jesterRb.drag = _wallDrag;
        }
    }

    private void PlayerWallJump()
    {   
        float horizontalForce = 0;
        
        RaycastHit2D leftWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.left, .01f, _wallLayerMask);
        RaycastHit2D rightWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.right, .01f, _wallLayerMask);
        
        if(leftWallHit.collider != null)
            horizontalForce = 1;
        else if(rightWallHit.collider != null)
            horizontalForce = -1;

        if (horizontalForce < 0)
            _jesterSpriteRenderer.flipX = true;
        else if (horizontalForce > 0)
            _jesterSpriteRenderer.flipX = false;

        float horiontalJump = _jesterLavaStats.GetLavaJesterJump() * horizontalForce / 2;
        float verticalJump = _jesterLavaStats.GetLavaJesterJump();

        _jesterRb.drag = 0;
        _isSliding = false;
        _grounded = false;
        _midairTimer = 0.3f;
        _jesterRb.AddForce(new Vector2(horiontalJump, verticalJump));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            if(!_grounded)
                _isSliding= true;
            _jesterRb.drag = _wallDrag;
        }


        RaycastHit2D raycastHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.down, .05f, _groundLayerMask);

        if (raycastHit)
        {
            _isSliding = false;
            _jesterRb.drag = 0;
        }

        _grounded = raycastHit.collider != null;
    }
}
