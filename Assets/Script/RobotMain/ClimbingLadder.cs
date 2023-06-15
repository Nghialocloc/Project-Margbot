using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingLadder : MonoBehaviour
{
    [Header("Check Condition Components")]
    [SerializeField] private bool isNearLadder;
    [SerializeField] private bool isClimbing;
    [SerializeField] private bool isOccupie;
    [SerializeField] private float climbingSpeed;
    private Rigidbody2D rb;
    private float verticalClimb;
    private float originGravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isClimbing = false;
        isNearLadder = false;
        originGravity = GetComponent<Rigidbody2D>().gravityScale; 
    }

    private void Update()
    {
        // If player near ladder and press up, allow climb
        verticalClimb = Input.GetAxis("Vertical");
        if (isNearLadder && Mathf.Abs(verticalClimb) >= 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        // As long climing is true, disable gravity
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, verticalClimb*climbingSpeed);
        }
        else
        {
            rb.gravityScale = originGravity;
        }
    }

    // Make sure player can climb if holding object like magnet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOccupie = GetComponent<RobotCarrying>().isCarryingMagnet;
        if (collision.gameObject.CompareTag("Ladder") && !isOccupie)
        {
            isNearLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isNearLadder = false;
            isClimbing = false;
        }
    }
}
