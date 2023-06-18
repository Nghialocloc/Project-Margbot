using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotCarrying : MonoBehaviour
{
    [Header("Pick up magnet")]
    [SerializeField] private bool isTouchingMagnet = false;
    [SerializeField] public bool isCarryingMagnet = false;
    [SerializeField] private Transform detectPoint;
    [SerializeField] private float detectRange = 0.2f;

    [Header("Component")]
    [SerializeField] private BoxCollider2D robotCollider;
    [SerializeField] private BoxCollider2D checkCollider;
    [SerializeField] private Rigidbody2D Magnet;
    //[SerializeField] private TrajectoryLine Joint2D;

    //When the player spawn in, find the componet and assign value (Only need if you want online mode)
    //private void Awake()
    //{
    //    Magnet = GameObject.FindGameObjectWithTag("Magnet").GetComponent<Rigidbody2D>();
    //    Joint2D = GameObject.FindGameObjectWithTag("Magnet").GetComponent<TrajectoryLine>();
    //    Joint2D.magnetJoin = GetComponent<FixedJoint2D>();
    //}

    // Player can not collide with the magnet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Magnet")
        {
            Physics2D.IgnoreCollision(collision.collider, robotCollider);
            Physics2D.IgnoreCollision(collision.collider, checkCollider);
        }
    }

    private void Update()
    {
        // Ray cast to check if the player is near the magnet
        RaycastHit2D pickupInfo = Physics2D.Raycast(detectPoint.position, transform.forward, detectRange);
        if (pickupInfo.collider != null && pickupInfo.collider.gameObject.tag == "Magnet")
        {
            isTouchingMagnet = true;
        }
        else
        {
            isTouchingMagnet = false;
        }

        // Press to enable Fixjoint and grab the magnet
        if (Input.GetButtonDown("GrabMagnet") && !PauseMenu.isPause)
        {
            if (!isCarryingMagnet && isTouchingMagnet)
            {
                GetComponent<FixedJoint2D>().enabled = true;
                GetComponent<FixedJoint2D>().connectedBody = Magnet;
                Magnet.GetComponent<TrajectoryLine>().enabled = true;
                Magnet.gravityScale = 1.5f;
                Magnet.mass = 1;
             }

            if (isCarryingMagnet)
            {
                Magnet.GetComponent<TrajectoryLine>().enabled = false;
                GetComponent<FixedJoint2D>().enabled = false;
                Magnet.gravityScale = 3;
                Magnet.mass = 3;
            }
        }

        CheckCarryingMagnet();
    }

    // Show the pick up range on unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(detectPoint.position ,detectRange);
    }

    // Check if the player is carrying the magent and make sure change the variable
    private void CheckCarryingMagnet()
    {
        if (GetComponent<FixedJoint2D>().enabled == true)
        {
            isCarryingMagnet = true;
        }
        else if (GetComponent<FixedJoint2D>().enabled == false)
        {
            isCarryingMagnet = false;
        }
    }
}
