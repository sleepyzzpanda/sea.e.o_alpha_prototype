using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    //other scripts can only read this value, but not modify
    public bool dialogueIsPlaying {get; private set;}

    private static DialogueManager instance;

    //since continuing text and entering text is same button, need variable to prevent entering dialogue again after pressing "z" to continue
    private bool dialogueDone;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than 1 dialogue manager in scene");
        }

        instance = this;
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        dialogueDone = false;

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //reset so can start dialogue again
        dialogueDone = false;
        //Debug.Log(dialogueIsPlaying);
        if(!dialogueIsPlaying)
        {
            return;
        }

        //handle continuing to next line in dialogue when submit is pressed
        if(currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        //prevent entering dialogue if just finished dialogue conversation
        if(dialogueDone) 
        {
            return;
        }

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        Debug.Log("A");

        //ContinueStory();
    }

    private void ExitDialogueMode()
    {
        
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        Debug.Log(dialogueIsPlaying);
        dialogueText.text = "";
        dialogueDone = true;
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            //set text for current dialogue line
            dialogueText.text = currentStory.Continue(); //give next line of dialogue
            //display choices if any
            DisplayChoices();
        }

        //end of ink file, no more text
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        //defensive check to make sure ui can support num of choices
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("more choices given than able to support. num choices: " + currentChoices.Count);
        }

        int index = 0;
        //enable and initialize the choices up to the amount of choices for this line of Dialogue
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //go throuhg remaing choices the ui supports and make sure theyre hidden
        for(int i = index; i <choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        //event system requires to be cleared first then wait for at least 1 frame before setting current selected obj
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    //so dialogue doesnt end after making choice
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        InputManager.GetInstance().RegisterSubmitPressed(); //if using the unity action script thing
        ContinueStory();
    }
}
