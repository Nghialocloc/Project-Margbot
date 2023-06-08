using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellingChecker : MonoBehaviour
{
    [SerializeField] public static bool isHitCelling = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 || collision.gameObject.layer == 6) && !collision.gameObject.CompareTag("Magnet"))
        {
            isHitCelling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHitCelling = false;
    }
}
