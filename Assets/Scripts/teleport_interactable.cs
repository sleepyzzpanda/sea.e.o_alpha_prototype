using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_interactable : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public GameObject player;
    public Transform new_pos, new_cam;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        //cant make dialogue play unless in range and dialogue wasnt playing alr
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            
            if(InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                player.transform.position = new_pos.position;
                player.GetComponent<player_behavior>().move_point.position = new_pos.position;
                Camera.main.transform.position = new_cam.position;
            }
            
        }

        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //check if nearby object is player to show visual cue
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //get rid of visual cue
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
