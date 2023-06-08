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
            Invoke("ResetLevel", 2);
        }
    }

    private void Update()
    {
        Endlevel();
    }
}
