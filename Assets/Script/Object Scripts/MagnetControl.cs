using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetControl : MonoBehaviour
{
    [SerializeField] private AreaEffector2D buttonArea;
    [SerializeField] private GameObject buttonEffect;
    [SerializeField] private bool isTurnOn = true;
    private ParticleSystem spawnEffect;
    private BoxCollider2D fieldEffect;

    private void Start()
    {
        spawnEffect = buttonEffect.GetComponent<ParticleSystem>();
        fieldEffect = GetComponent<BoxCollider2D>();
        // Set how the magnet active in the beginsceen
        if (isTurnOn)
        {
            fieldEffect.enabled = true;
            buttonArea.enabled = true;
            buttonEffect.SetActive(true);
            spawnEffect.Play();
            isTurnOn = true;

        }
        else
        {
            fieldEffect.enabled = false;
            buttonArea.enabled = false;
            buttonEffect.SetActive(false);
            isTurnOn = false;
        }
    }

    public void TurnOnandOff()
    {
        if (isTurnOn)
        {
            fieldEffect.enabled = false;
            buttonArea.enabled = false;
            buttonEffect.SetActive(false);
            isTurnOn = false;
            
        }
        else
        {
            fieldEffect.enabled = true;
            buttonArea.enabled = true;
            buttonEffect.SetActive(true);
            spawnEffect.Play();
            isTurnOn = true;
        }
    }
}
