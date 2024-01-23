using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester_LavaMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _jesterRb;
    [SerializeField] private Collider2D _jesterCollider;
    [SerializeField] private Jester_Lava_Stats _jesterLavaStats;
    [SerializeField] private LayerMask _groundLayerMask;
    private bool isGrounded;

    void Update()
    {
        PlayerMovement();

        Debug.DrawRay(_jesterCollider.bounds.center + new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, 0), Vector2.down * (_jesterCollider.bounds.extents.y + 0.01f));
        Debug.DrawRay(_jesterCollider.bounds.center - new Vector3(_jesterCollider.bounds.extents.x, _jesterCollider.bounds.extents.y), Vector2.right * (_jesterCollider.bounds.extents.x));

        if(Input.GetKeyDown(KeyCode.Space) && IsPlayerGrouded())
            PlayerJump();
    }

    private void PlayerMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }

        inputVector = inputVector.normalized;

        transform.position += (Vector3)inputVector * Time.deltaTime * _jesterLavaStats.GetLavaJesterSpeed();
    }

    private void PlayerJump()
    {
        _jesterRb.AddForce(new Vector2(0, _jesterLavaStats.GetLavaJesterJump()), ForceMode2D.Impulse);
    }

    private bool IsPlayerGrouded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_jesterCollider.bounds.center, _jesterCollider.bounds.size, 0f, Vector2.down, 0.05f, _groundLayerMask);
        
        return raycastHit.collider != null;
    }
}
