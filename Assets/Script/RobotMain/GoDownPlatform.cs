using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDownPlatform : MonoBehaviour
{
    private GameObject oneWayPlatform;
    [SerializeField] private BoxCollider2D playerCollision;
    [SerializeField] private BoxCollider2D groundcheckCollision;
    [SerializeField] private FixedJoint2D carryMagnet;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("GoDown"))
        {
            if (oneWayPlatform != null && carryMagnet.enabled == false)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    // Check if player is standing on one way platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            oneWayPlatform = collision.gameObject;
        }
    }

    // Uncheck it when we left the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            oneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D collisionPlat = oneWayPlatform.GetComponent<BoxCollider2D>();

        groundcheckCollision.enabled = false;
        Physics2D.IgnoreCollision(playerCollision, collisionPlat);
        yield return new WaitForSeconds(0.25f);
        groundcheckCollision.enabled = true;
        Physics2D.IgnoreCollision(playerCollision, collisionPlat,false);
    }
}
