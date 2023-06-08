using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    [Header("Button property")]
    [SerializeField] [Range(0.01f, 0.2f)] private float pressSpeed = 0.01f;
    //[SerializeField] private float maxDistance = 2f;
    [SerializeField] private BoxCollider2D topChecker;
    [SerializeField] private LayerMask objectPress;
    [SerializeField] private bool moveBack;
    [SerializeField] private bool isPress;


    [Header("Event")]
    [SerializeField] public GameEvent onPress;
    [SerializeField] public GameEvent onRelease;

    private Vector3 originPosition;

    private void Start()
    {
        isPress = false;
        moveBack = false;
        originPosition = gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check collision with object only
        if(collision.gameObject.layer != 3)
        {
            if (!isPress)
            {
                collision.transform.parent = transform;
                onPress.Call();
            }
            isPress = true;
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (originPosition.y - transform.position.y <= maxDistance)
    //    {
    //        transform.Translate(0, -pressSpeed, 0);
    //        moveBack = false;
    //    }
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
        if (!isOnTop())
        {
            moveBack = true;
            onRelease.Call();
            isPress = false;
        }
    }

    //Check if there is any thing on the button
    private bool isOnTop()
    {
        return Physics2D.BoxCast(topChecker.bounds.center, topChecker.bounds.size, 0f, Vector2.up, 0.1f,objectPress);
    }

    // Move back the button when nothing on it (Not using right now)
    private void Update()
    {
        if (moveBack && (transform.position.y < originPosition.y))
        {
            transform.Translate(0, pressSpeed * 1.5f, 0);
        }
        else
        {
            moveBack = false;
        }
    }
}
