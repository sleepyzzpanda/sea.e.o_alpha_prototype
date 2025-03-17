using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public GameObject player;
    public Transform floor1pos, floor1cam, floor2pos, floor2cam;

    public AudioSource audioSource;
    public AudioClip audioClip;

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
                // play audio
                audioSource.PlayOneShot(audioClip);
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                int floor = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("current_floor")).value;
                if(floor == 2){ // go to floor 1
                    player.transform.position = floor1pos.position;
                    player.GetComponent<player_behavior>().move_point.position = floor1pos.position;
                    Camera.main.transform.position = floor1cam.position;
                    // set flag to 1
                    ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("current_floor")).value = 1;
                }
                else if(floor == 1){ // go to floor 2
                    player.transform.position = floor2pos.position;
                    player.GetComponent<player_behavior>().move_point.position = floor2pos.position;
                    Camera.main.transform.position = floor2cam.position;
                    // set flag to 2
                    ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("current_floor")).value = 2;
                }
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
