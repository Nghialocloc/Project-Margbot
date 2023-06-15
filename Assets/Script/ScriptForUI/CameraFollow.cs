using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [Header("Camare zone")]
    [SerializeField] private float boundX = 20f;
    [SerializeField] private float boundY = 14f;
    [Header("Change the center camera")] // Broken right now so don't use it
    [SerializeField] [Range(-1f,1f)] private float offSetX = 0;
    [SerializeField] [Range(-1f,1f)] private float offSetY = 0;

    private void Update()
    {
        Vector2 delta = Vector2.zero;
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX + offSetX;
            }
            else
            {
                delta.x = deltaX + boundX + offSetX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY + offSetY;
            }
            else
            {
                delta.y = deltaY + boundY + offSetY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
