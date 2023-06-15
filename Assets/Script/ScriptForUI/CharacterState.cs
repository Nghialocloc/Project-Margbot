using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private GameEvent levelOver;
    [SerializeField] private GameObject playerCharacter;

    private void ResetLevel()
    {
        levelOver.Call();
    }

    private void Endlevel()
    {
        if (RobotMovement.isDead)
        {
            playerCharacter.SetActive(false);
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySfx("GameOver");
            Debug.Log("Die");
            Invoke("ResetLevel", 3);
            AudioManager.Instance.PlayMusic("Theme");
        }
    }

    private void Update()
    {
        Endlevel();
    }
}
