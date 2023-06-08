using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    [Header("Moving property")]
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving = false;
    Vector3 targetPos; 

    private RobotMovement movementControl;
    private Rigidbody2D rbPlatform;
    private Vector3 moveDirection;

    private void Awake()
    {
        movementControl = GameObject.FindGameObjectWithTag("Player").GetComponent<RobotMovement>();
        rbPlatform = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = positionA.position;
        DirectionCaculate();
    }

    public void MovePlatform()
    {
        if (!isMoving)
        {
            isMoving = true;
            while (Vector2.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);  
            }
            isMoving = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, positionA.position) < 0.1f)
        {
            targetPos = positionB.position;
            DirectionCaculate();
        }

        else if (Vector2.Distance(transform.position, positionB.position) < 0.1f)
        {
            targetPos = positionA.position;
            DirectionCaculate();
        }

        if (Vector2.Distance(transform.position, targetPos) == 0)
        {
            rbPlatform.velocity = moveDirection * 0;
        }
    }

    // Change the direction base on the target position
    private void DirectionCaculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    // Allow player to move smoothly on the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            movementControl.isOnPlatform = true;
            movementControl.platformRB = rbPlatform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        movementControl.isOnPlatform = false;
        movementControl.platformRB = null;
    }
}
