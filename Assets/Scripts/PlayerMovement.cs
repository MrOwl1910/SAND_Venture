using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    private float horizontal;
    private bool isfacingright = true;


    private float Speed = 2f;
    private float JumpPower = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);

        if (!isfacingright && horizontal > 0f)
        {
            flip();
        }
        else if ( isfacingright && horizontal < 0f )
        {
            flip ();
        }
    }

    //Ground Check
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.5f, GroundLayer);
    }

    //FLip
    private void flip()
    {
        isfacingright = !isfacingright;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f;
        transform.localScale = localscale;
    }


    //Movement 
    public void Onmove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;

        Debug.Log("isMoving");
    }

    //Jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded()) 
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity =new Vector2(rb.velocity.x,rb.velocity.y * 0.5f);
        }

        Debug.Log("isJumping");
    }

}//class
