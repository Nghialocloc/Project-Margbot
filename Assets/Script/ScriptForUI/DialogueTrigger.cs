using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   [SerializeField] private GameObject dialogue;
   private GameObject character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogue.SetActive(true);
            character = GameObject.FindGameObjectWithTag("Player");
            character.GetComponent<RobotMovement>().enabled = false;
            character.GetComponent<RobotCarrying>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}
