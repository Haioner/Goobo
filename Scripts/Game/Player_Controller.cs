using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float speed;
    bool isFacingRight;
    [HideInInspector] public bool canMove = true;
    [HideInInspector]public float initialSpeed;

    [Header("Jump")]
    public LayerMask ground;
    public float jumpForce;
    public float radius;
    [HideInInspector] public bool isGrounded;
    public AudioSource jumpSource;
    bool inAir = false;

    [Header("Wall Jump")]
    public Transform wallCheck;
    public float wallRadius;
    public float wallSlideSpeed;
    bool isWallSliding;
    bool isTouchingWall;
    int facingDirection = 1;
    public Vector2 wallJumpDirection;
    public ParticleSystem dustParticle;

    [Header("Cache")]
    public Animator anim;
    public PlayerPullAndPush playerPullAndPush;

    private void Awake()
    {
        initialSpeed = speed;
    }

    void Update()
    {
        if (canMove)
        {
            Movement();
            WalkAnim();
        }
        Jump();
        CheckWall();
        CheckWallSliding();
        WallJump();
    }

    void Flip()
    {
        if(playerPullAndPush.box == null)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    public void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if (x > 0 && isFacingRight)
            Flip();
        else if (x < 0 && !isFacingRight)
            Flip();
    }

    public void WalkAnim()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float absHorizontal = Mathf.Abs(x);
        if (absHorizontal > 0 && isGrounded)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);

        anim.SetBool("IsGrounded", isGrounded);

    }

    public void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, radius, ground);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && playerPullAndPush.box == null)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump");
            jumpSource.Play();
        }

        if (!isGrounded)
            inAir = true;

        if(inAir && isGrounded)
        {
            jumpSource.Play();
            inAir = false;
        }
    }

    void CheckWall()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallRadius, ground);
    }

    void CheckWallSliding()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (isTouchingWall && !isGrounded)
            isWallSliding = true;
        else
            isWallSliding = false;

        anim.SetBool("WallSliding", isWallSliding);

        var emission = dustParticle.emission;
        if (isWallSliding && rb.velocity.y < -wallSlideSpeed)
        {
            emission.rateOverTime = 4;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
          
        }
        else
            emission.rateOverTime = 0;
    }

    void WallJump()
    {
        if (isWallSliding && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("WallJump");
            Vector2 force = new Vector2(jumpForce * wallJumpDirection.x * -facingDirection, jumpForce * wallJumpDirection.y);
            Flip();
            rb.velocity = Vector2.zero;
            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine("StopMove");
            jumpSource.Play();
        }
    }

    IEnumerator StopMove()
    {
        canMove = false;
        yield return new WaitForSeconds(.3f);
        transform.localScale = Vector2.one;
        canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallCheck.position, transform.right * wallRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
