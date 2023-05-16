using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMagnet : MonoBehaviour
{

    [Header("Magnet property")]
    [SerializeField] private bool isStatic = false;
    [SerializeField] private SpriteRenderer magnetColor;

    // Start is called before the first frame update
    void Start()
    {
        magnetColor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ChangeMagnet") && !PauseMenu.isPause)
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        if (!isStatic)
        {
            isStatic = true;
        }
        else
        {
            isStatic = false;
        }
    }
}
