using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMagnet : MonoBehaviour
{

    [Header("Magnet property")]
    [SerializeField] private bool isAttract;

    [SerializeField] private SpriteRenderer magnetColor;
    [SerializeField] private Sprite magnetBlue;
    [SerializeField] private Sprite magnetRed;

    [Header("Force property")]
    [SerializeField] private AreaEffector2D forceField;

    [Header("Effect color")]
    [SerializeField] private GameObject forceBlue;
    [SerializeField] private GameObject forceRed;

    private void Start()
    {
        if (isAttract)
        {
            magnetColor.sprite = magnetRed;
            forceBlue.SetActive(false);
            forceRed.SetActive(true);
            forceRed.GetComponent<ParticleSystem>().Play();
            RotateField();
            isAttract = false;
        }
        else
        {
            magnetColor.sprite = magnetBlue;
            forceRed.SetActive(false);
            forceBlue.SetActive(true);
            forceBlue.GetComponent<ParticleSystem>().Play();
            isAttract = true;
        }
    }

    public void ChangeState()
    {
        if (isAttract)
        {
            magnetColor.sprite = magnetRed;
            forceBlue.SetActive(false);
            forceRed.SetActive(true);
            forceRed.GetComponent<ParticleSystem>().Play();
            RotateField();
            isAttract = false;
        }
        else
        {
            magnetColor.sprite = magnetBlue;
            forceRed.SetActive(false);
            forceBlue.SetActive(true);
            forceBlue.GetComponent<ParticleSystem>().Play();
            RotateField();
            isAttract = true;
        }
    }

    private void RotateField()
    {
        if(forceField.forceAngle >= 180)
        {
            forceField.forceAngle = forceField.forceAngle - 180;
        }
        else
        {
            forceField.forceAngle = forceField.forceAngle + 180;
        }
    }
}
