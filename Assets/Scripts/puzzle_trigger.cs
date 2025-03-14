using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle_trigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public GameObject puzzle, puzzle_manager, wrongEventSystem, blocking_collider;

    private bool playerInRange, puzzle_done;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        puzzle_done = false;
        puzzle.SetActive(false);
    }

    private void Update()
    {
        //cant make dialogue play unless in range and dialogue wasnt playing alr
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            
            if(InputManager.GetInstance().GetInteractPressed())
            {
                puzzle.SetActive(true);
                wrongEventSystem.SetActive(false);
            }
            
        }

        else
        {
            visualCue.SetActive(false);
        }
        puzzle_done = puzzle_manager.GetComponent<PuzzleManager>().puzzleComplete;
        if(puzzle_done)
        {
            // if ink json not null, enter dialogue mode
            if(inkJSON != null)
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                
            puzzle.SetActive(false);
            visualCue.SetActive(false);
            gameObject.SetActive(false);
            wrongEventSystem.SetActive(true);
            blocking_collider.SetActive(false);
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
