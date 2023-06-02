using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    [Header("Moving property")]
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isatpointA = false;
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
        rbPlatform.velocity = moveDirection * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // If platfrom is moving toward A point, check mark that it's in A
        if ((Vector2.Distance(transform.position, positionA.position) < 0.1f) && !isatpointA)
        {
            targetPos = positionB.position;
            DirectionCaculate();
            rbPlatform.velocity = Vector2.zero;
            isatpointA = true;
        }

        // And reverse if it's move toward B point
        else if ((Vector2.Distance(transform.position, positionB.position) < 0.1f) && isatpointA)
        {
            targetPos = positionA.position;
            DirectionCaculate();
            rbPlatform.velocity = Vector2.zero;
            isatpointA = false;
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
