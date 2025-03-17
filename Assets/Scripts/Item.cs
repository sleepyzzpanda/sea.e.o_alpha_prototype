using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite itemSprite;
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    private InventoryManager inventoryManager;
    public GameObject container;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

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
                inventoryManager.AddItem(itemName, quantity, itemSprite);
                
                audioSource.PlayOneShot(audioClip);
                // set item to inactive
                container.SetActive(false);
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
