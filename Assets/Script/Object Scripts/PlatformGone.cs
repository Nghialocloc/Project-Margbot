using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGone : MonoBehaviour
{
    [SerializeField] private bool isHidden;
    [SerializeField] private GameObject[] platformIn;

   private void Start()
    {
        PlatformActive();
    }

    public void PlatformActive()
    {
        if (isHidden)
        {
            foreach (GameObject platform in platformIn)
            {
                platform.SetActive(true);
            }
            isHidden = false;
        }
        else
        {
            foreach (GameObject platform in platformIn)
            {
                platform.SetActive(false);
            }
            isHidden = true;
        }
    }
}
