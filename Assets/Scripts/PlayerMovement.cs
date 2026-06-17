using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Vector2 moveInput;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        Debug.Log("Sprite found: " + sprite.name);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if(IsGrounded() && value.isPressed)
        {
            body.linearVelocity = new Vector2(
                body.linearVelocity.x,
                jumpForce
            );
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
    }

    public void FixedUpdate()
    {
        if(moveInput.x > 0)
        {
            sprite.flipX = false;
        }
        if (moveInput.x < 0)
        {
            sprite.flipX = true;
        }

        body.linearVelocity = new Vector2(
            moveInput.x * speed,
            body.linearVelocity.y 
        );
    }
}
