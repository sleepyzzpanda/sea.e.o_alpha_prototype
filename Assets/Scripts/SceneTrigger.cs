using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool playerInRange, has_triggered;
    private void Awake()
    {
        playerInRange = false;
        has_triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && !has_triggered)
        {
            has_triggered = true;
            // trigger dialogue if player is in range and dialogue is not playing
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            // set this object to inactive after dialogue is triggered
            gameObject.SetActive(false);
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
