using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4.5f;
    [SerializeField] private float sprintSpeed = 4.8f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask groundLayer;

    [Header("Jump Buffer")]
    [SerializeField] private float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    [Header("Variable Jump Height")]
    [SerializeField] private float jumpCutMultiplier = 0.5f;

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackDistance = 0.8f;

    // Component References
    private Rigidbody2D rb;

    // Movement
    private float horizontalInput;
    private float currentMoveSpeed;

    // Direction
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        //-------------------------
        // Movimento horizontal
        //-------------------------
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Atualiza a direção do personagem
        Flip(horizontalInput);

        //-------------------------
        // Sprint (Segurar Shift)
        //-------------------------
        if (Input.GetKey(KeyCode.LeftShift))
            currentMoveSpeed = sprintSpeed;
        else
            currentMoveSpeed = moveSpeed;

        //-------------------------
        // Jump Buffer
        //-------------------------
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        //-------------------------
        // Coyote Time
        //-------------------------
        if (IsGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        //-------------------------
        // Executa o pulo
        //-------------------------
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }

        //-------------------------
        // Variable Jump Height
        //-------------------------
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                rb.velocity.y * jumpCutMultiplier
            );
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(
            horizontalInput * currentMoveSpeed,
            rb.velocity.y
        );
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(
            groundCheckPoint.position,
            groundCheckSize,
            0f,
            groundLayer
        );
    }

    private void Flip(float direction)
    {
        if (direction > 0 && !facingRight)
        {
            facingRight = true;
            spriteRenderer.flipX = false;
            attackPoint.localPosition = new Vector3(attackDistance, 0f, 0f);
        }
        else if (direction < 0 && facingRight)
        {
            facingRight = false;
            spriteRenderer.flipX = true;
            attackPoint.localPosition = new Vector3(-attackDistance, 0f, 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
    }
}
