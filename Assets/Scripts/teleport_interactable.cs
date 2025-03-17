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
    private GameObject player_sprite;
    public Transform new_pos, new_cam;
    public bool diving;

    private bool playerInRange;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Awake()
    {
        player_sprite = player.GetComponent<player_behavior>()._sprite;
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
                audioSource.PlayOneShot(audioClip);
                player.GetComponent<player_behavior>().is_diving = diving;
                if(diving){
                    player_sprite.GetComponent<SpriteRenderer>().sprite = player.GetComponent<player_behavior>().diving_front[0];
                } else {
                    player_sprite.GetComponent<SpriteRenderer>().sprite = player.GetComponent<player_behavior>().normal_front[0];
                }
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
