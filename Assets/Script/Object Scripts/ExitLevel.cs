using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private Animator anima;
    private BoxCollider2D blockCheck;

    [Header("Check unlock level")]
    [SerializeField] private int newLevelUnlock;

    [Header("Door interact")]
    [SerializeField] private bool isNearDoor = false;
    [SerializeField] private Transform outLevelCheck;

    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        blockCheck = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStateDoor();
        CheckExitBox();
    }

    #region Image and Animation

    private enum MovementState { static_state, open_state, close_state }
    MovementState stateDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearDoor = true;
        }
    }

    // if player go away from the door, close it and active the collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stateDoor = MovementState.close_state;
            isNearDoor = false;
            blockCheck.enabled = true;
        }
    }

    private void CheckStateDoor()
    {
        // If player is near the door and interact, disable collider and open the door
        if (isNearDoor && Input.GetButtonDown("Interact"))
        {
            stateDoor = MovementState.open_state;
            blockCheck.enabled = false;
        }
        anima.SetInteger("Door", (int)stateDoor);
    }

    #endregion

    #region ExitZone

    [Header("Exit level poprety")]
    [SerializeField] private float trasBoxWidth;
    [SerializeField] private float trasBoxHeight;
    [SerializeField] public GameObject winMenu;
    [SerializeField] public GameObject pauseManager;

    // Make the transfet box 
    private void CheckExitBox()
    {
        Collider2D[] collision = Physics2D.OverlapBoxAll(outLevelCheck.position, new Vector2(trasBoxWidth, trasBoxHeight), 0);
        foreach(Collider2D hit in collision)
        {
            if (hit.gameObject.CompareTag("Magnet"))
            {
                hit.gameObject.SetActive(false);
            }
            if (hit.gameObject.CompareTag("Player"))
            {
                hit.gameObject.SetActive(false);
                pauseManager.SetActive(false);
                winMenu.SetActive(true);
                PlayerPrefs.SetInt("levelReached", newLevelUnlock);
                AudioManager.Instance.musicSource.Stop();
                AudioManager.Instance.PlaySfx("LevelComplete");
            }
        }
    }

    // Show the transfet box on scene
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(outLevelCheck.position, new Vector2(trasBoxWidth, trasBoxHeight));
    }

    #endregion
}
