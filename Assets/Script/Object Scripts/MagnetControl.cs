using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetControl : MonoBehaviour
{
    [SerializeField] private AreaEffector2D buttonArea;
    [SerializeField] private GameObject buttonEffect;
    [SerializeField] private bool isTurnOn = true;
    private ParticleSystem spawnEffect;

    private void Start()
    {
        spawnEffect = buttonEffect.GetComponent<ParticleSystem>();
    }

    public void TurnOnandOff()
    {
        if (isTurnOn)
        {
            buttonArea.enabled = false;
            buttonEffect.SetActive(false);
            isTurnOn = false;
            
        }
        else
        {
            buttonArea.enabled = true;
            buttonEffect.SetActive(true);
            spawnEffect.Play();
            isTurnOn = true;
        }
    }
}
