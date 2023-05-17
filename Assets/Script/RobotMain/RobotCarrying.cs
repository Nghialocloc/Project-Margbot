using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotCarrying : MonoBehaviour
{
    [Header("Pick up magnet")]
    [SerializeField] private bool isTouchingMagnet = false;
    [SerializeField] private bool isCarryingMagnet = false;
    [SerializeField] private bool isInogreMagnet = false;
    [SerializeField] private Transform detectPoint;
    [SerializeField] private float detectRange = 0.2f;

    [Header("Component")]
    [SerializeField] private BoxCollider2D robotCollider;
    [SerializeField] private BoxCollider2D checkCollider;
    [SerializeField] private BoxCollider2D magnetCollider;
    [SerializeField] public Rigidbody2D Magnet;

    // Player can not collide with the magnet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Magnet")
        {
            isInogreMagnet = true;
            Physics2D.IgnoreCollision(magnetCollider,robotCollider);
            Physics2D.IgnoreCollision(magnetCollider, checkCollider);
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
                Magnet.gravityScale = 1.5f;
             }

            if (isCarryingMagnet)
            {
                GetComponent<FixedJoint2D>().enabled = false;
                Magnet.gravityScale = 3;
            }
        }

        CheckCarryingMagnet();
    }

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
