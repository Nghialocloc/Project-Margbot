using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxKill : MonoBehaviour
{
    private string currLevel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Magnet" || collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Invoke("Reset", 2);
        }
    }

    private void Reset()
    {
        currLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currLevel);
    }
}
