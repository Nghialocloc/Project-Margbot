using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D checkCollision;
    [SerializeField] [Range(0.1f, 0.3f)] private float rayBuffer = 0.2f;
    public static bool isDead = false;

    [Header("Moving")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 7;
    private float moveInput;
    private bool isFacingRight = true;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask jumpableGround;

    [Header("Jumping")]
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("Fall Physics")]
    public float fallMultiplier = 5;
    public float lowJumpMultiplier = 2;

    [Header("On platform movemnet")]
    [SerializeField] public bool isOnPlatform;
    [HideInInspector] public Rigidbody2D platformRB;

    //Gets Rigidbody component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
    }

    //Moves player on x axis
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if(isOnPlatform)
        {
            rb.velocity = new Vector2(moveInput * speed + platformRB.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    void Update()
    {
        controlMovement();
        CharacterStuck();
    }

    private bool isGround()
    {
        return Physics2D.BoxCast(checkCollision.bounds.center, checkCollision.bounds.size, 0f, Vector2.down, rayBuffer, jumpableGround);
    }

    //Check if the character being stuck between the box and map

    private void CharacterStuck()
    {
        if (isGround() && CellingChecker.isHitCelling)
        {
            isDead = true;
        }
    }

    #region Movement

    private void controlMovement()
    {
        //turn around the direction you go
        if (moveInput > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (moveInput < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        //cool jump fall ( not cool enough through )
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Disable this if want double jump 
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        //lets player jump
        if (isGround() == true && Input.GetButtonDown("Jump") && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            AudioManager.Instance.PlaySfx("Jump");
        }

        //makes you jump higher when you hold down jump button
        if (Input.GetButton("Jump") && isJumping == true)
        {

            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;

            }
        }
    }

    #endregion

}