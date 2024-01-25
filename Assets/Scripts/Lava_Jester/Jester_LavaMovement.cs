using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jester_LavaMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _jesterRb;
    [SerializeField] private Collider2D _jesterCollider;
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _wallLayerMask;
    [SerializeField] private float _wallDrag;
    private float _movementInput;
    private bool _isSliding = false;

    void Update()
    {
        //Debug.DrawRay(_jesterCollider.bounds.center + new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        //Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        //Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, _jesterCollider.bounds.extents.y), Vector2.right * (_jesterCollider.bounds.extents.x));

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

        RaycastHit2D leftWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.left, 0.01f, _wallLayerMask);
        RaycastHit2D rightWallHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.right, 0.01f, _wallLayerMask);
        
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

    private void PlayerMovement2()
    {
        float _inputForce = Input.GetAxisRaw("Horizontal");
        Vector2 movementVector = new Vector2(_inputForce * _jesterLavaStats.GetLavaJesterSpeed() * Time.deltaTime, _jesterRb.velocity.y);

        _jesterRb.velocity = movementVector;
    }

    private void PlayerJump()
    {
        _jesterRb.AddForce(new Vector2(0, _jesterLavaStats.GetLavaJesterJump()), ForceMode2D.Impulse);
    }

    private IEnumerator PlayerWallJump()
    {   
        float horizontalForce = 0;
        if(transform.position.x > 0)
            horizontalForce = -1;
        else if(transform.position.x < 0)
            horizontalForce = +1;
        
        float horiontalJump = _jesterLavaStats.GetLavaJesterJump()/4 * horizontalForce;

        _jesterRb.velocity = Vector2.zero;
        _jesterRb.gravityScale = 1f;
        _jesterRb.AddForce(new Vector2(horiontalJump, _jesterLavaStats.GetLavaJesterJump()), ForceMode2D.Impulse);
        _isSliding = false;

        yield return new WaitForSeconds(1f);        
    }

    private bool IsPlayerGrouded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.down, 0.05f, _groundLayerMask);

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
