using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jester_LavaMovement : MonoBehaviour
{
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    private Rigidbody2D _jesterRb => _jesterLavaStats.GetJesterRigidBody();
    private Collider2D _jesterCollider => _jesterLavaStats.GetJesterCollider();
    private LayerMask _groundLayerMask => _jesterLavaStats.GetGroundLayerMask();
    private LayerMask _wallLayerMask => _jesterLavaStats.GetWallLayerMask();
    
    [SerializeField] private float _wallDrag;
    private float _movementInput;
    private bool _isSliding = false;

    void Update()
    {
        //Debug.DrawRay(_jesterCollider.bounds.center + new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        //Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        //Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, _jesterCollider.bounds.extents.y), Vector2.right * (_jesterCollider.bounds.extents.x));

        if(!_isSliding)
            PlayerMovement();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(IsPlayerGrouded() && !_isSliding)
            {
                PlayerJump();
            }
            else if(!IsPlayerGrouded() && _isSliding)
            {
                StartCoroutine(PlayerWallJump());
                _jesterRb.drag = 0;
            }
        }

        IncreaseGravity();
    }

    private void PlayerMovement()
    {
        Vector2 _inputVector = new Vector2(0, 0);

        RaycastHit2D leftWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.left, .01f, _wallLayerMask);
        RaycastHit2D rightWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.right, .01f, _wallLayerMask);
        
        //Debug.Log(_hitRighttWall);

        if(Input.GetKey(KeyCode.D) && rightWallHit.collider == null)
        {
            _inputVector.x += 1;
        }
        if(Input.GetKey(KeyCode.A) && leftWallHit.collider == null)
        {
            _inputVector.x -= 1;
        }

        _inputVector = _inputVector.normalized;  

        transform.position += (Vector3)_inputVector * _jesterLavaStats.GetLavaJesterSpeed() * Time.deltaTime;
    }

    private void PlayerJump()
    {
        _jesterRb.AddForce(new Vector2(0, _jesterLavaStats.GetLavaJesterJump()), ForceMode2D.Impulse);
    }

    private IEnumerator PlayerWallJump()
    {   
        float horizontalForce = 0;
        
        RaycastHit2D leftWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.left, .01f, _wallLayerMask);
        RaycastHit2D rightWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.right, .01f, _wallLayerMask);
        
        if(leftWallHit.collider != null)
            horizontalForce = +1;
        else if(rightWallHit.collider != null)
            horizontalForce = -1;

        float horiontalJump = _jesterLavaStats.GetLavaJesterJump() * horizontalForce;
        float verticalJump = _jesterLavaStats.GetLavaJesterJump();

        _jesterRb.velocity = Vector2.zero;
        _jesterRb.gravityScale = 1f;
        _jesterRb.AddForce(new Vector2(horiontalJump, verticalJump), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);

        _jesterRb.velocity = Vector2.zero;
        _isSliding = false;
    }

    private bool IsPlayerGrouded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.down, .05f, _groundLayerMask);

        if(raycastHit)
        {
            _isSliding = false;
            _jesterRb.drag = 0;
        }

        return raycastHit.collider != null;
    }
    
    private void IncreaseGravity()
    {
        if(_jesterRb.velocity.y < 0 && _jesterRb.velocity.y > -5)
            _jesterRb.gravityScale += 1f * Time.deltaTime;
        
        if(IsPlayerGrouded())
            _jesterRb.gravityScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            _jesterRb.velocity = Vector2.zero;
            _isSliding = false;
            _isSliding= true;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            _isSliding = true;
            _jesterRb.drag = _wallDrag;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
        {
            if(col.gameObject.tag == "Wall")
            {
                _isSliding = false;
                _jesterRb.drag = 0;
            }
        }
}
