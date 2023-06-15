using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScreen : MonoBehaviour
{
    [SerializeField] public GameEvent onActivate;
    [SerializeField] private bool isInteract = false;

    private void Update()
    {
        // Press the button to send active signal
        if (Input.GetButtonDown("Interact") && isInteract && !PauseMenu.isPause)
        {
            onActivate.Call();
        }
    }

    // If player near the panel, enable interact with it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Magnet"))
        {
            isInteract = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D[] check = Physics2D.OverlapBoxAll(gameObject.transform.position, new Vector2(1.5f, 1), 0);
        isInteract = false;
        foreach (Collider2D hit in check)
        {
            if (hit.gameObject.CompareTag("Magnet") || hit.gameObject.CompareTag("Player"))
                isInteract = true;
        }
    }
}
