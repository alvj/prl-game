using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpVelocity = 10f;
    [SerializeField] float fallMultiplier = 1.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    float hInput;

    [Space]

    [Header("Ground checking")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;

    // Components
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded == true) {
            rb.velocity = Vector2.up * jumpVelocity;
        }

        ManageAnimations();
    }

    private void FixedUpdate(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        hInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        ManageJump();
    }

    void ManageJump() {
        // If going down, multiply gravity to fall faster
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        // If going up and jumping button is released early, multiply gravity to get a lower jump
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void ManageAnimations() {
        anim.SetBool("isJumping", !isGrounded);

        if (hInput > 0.1f) {
            anim.SetBool("isWalking", true);
            sr.flipX = false;
        }
        else if (hInput < -0.1f) {
            anim.SetBool("isWalking", true);
            sr.flipX = true;
        }
        else {
            anim.SetBool("isWalking", false);
        }
    }
}