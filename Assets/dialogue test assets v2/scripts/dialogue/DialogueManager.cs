using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    //[Header("Globals Ink File")]
    //[SerializeField] private InkFile globalsInkFile;
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.02f;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private GameObject speakerBox;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo; //to make unique dialogue for each npc
    [SerializeField] private DialogueAudioInfoSO[] audioInfos; //dif dialogue configurations for NPCs
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary; //keep track of dialogue configurations for npcs
    private DialogueAudioInfoSO currentAudioInfo;
    
    private AudioSource audioSource;

    private Story currentStory;

    //other scripts can only read this value, but not modify
    public bool dialogueIsPlaying {get; private set;}

    //dialogue skipping
    private Coroutine displayLineCoroutine; //dialogue being typed
    private bool canContinueToNextLine = false;

    private static DialogueManager instance;

    //since continuing text and entering text is same button, need variable to prevent entering dialogue again after pressing "z" to continue
    private bool dialogueDone;

    //to set who is speaking
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";

    //to keep track of dialogue options
    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than 1 dialogue manager in scene");
        }

        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        //dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
        audioSource = this.gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
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
        InitializeAudioInfoDictionary();
    }

    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
        audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
        foreach(DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }

    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        if(audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else Debug.LogWarning("failed to find audio info for id: " + id);
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

        //handle continuing to next line in dialogue when submit is pressed and full dialogue line has been shown
        if(canContinueToNextLine && currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed()) //choice check to make sure line doesnt skip when selecting a choice
        {
            //Debug.Log("can continue");
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
        //Debug.Log("A");

        dialogueVariables.StartListening(currentStory); //start listening when story begins

        //rest portrait and speaker so it doesnt carry through from previous Dialogue
        displayNameText.text = "none";
        portraitAnimator.Play("default");

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueVariables.StopListening(currentStory); //stop listening when story ends

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        Debug.Log(dialogueIsPlaying);
        dialogueText.text = "";
        dialogueDone = true;
        
        //go back to default Audio
        SetCurrentAudioInfo(defaultAudioInfo.id);

        Debug.Log("dialogue finished");
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            Debug.Log("continuing story");
            //skip current line of dialogue if player pressed input button
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue(); //make sure audio handled first so that audio will switch when line start instead of using previous config
            //to set who is speaking
            HandleTags(currentStory.currentTags);
            //set text for current dialogue line
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine)); //give next line of dialogue
        }

        //end of ink file, no more text
        else
        {
            Debug.Log("end of ink file");
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        Debug.Log("handling tags");
        //loop through each tag
        foreach(string tag in currentTags)
        {
            //parse tags in key value pairs
            string[] splitTag = tag.Split(':'); //first string is key, second string is value

            //check if valid string
            if(splitTag.Length != 2) 
            {
                Debug.LogError("Tag couldnt be parsed: " + tag );
            }
            string tagKey = splitTag[0].Trim(); //key, trim to get rid of white space
            string tagValue = splitTag[1].Trim(); //value

            //handle the tag
            switch(tagKey) 
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    Debug.Log("speaker=" + tagValue);
                    
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue); //changes portrait sprite
                    Debug.Log("portrait=" + tagValue);
                    break;
                case LAYOUT_TAG:
                    Debug.Log("layout=" + tagValue);
                    break;
                case AUDIO_TAG:
                    SetCurrentAudioInfo(tagValue);
                    Debug.Log("layout=" + tagValue);
                    break;
                default: //in case not valid
                    Debug.LogWarning("tag came in but isnt currently being handled: " + tag);
                    break;
            }

        }

        if(displayNameText.text == "none" || displayNameText.text == "")
            {
                speakerBox.SetActive(false);
            }
        else speakerBox.SetActive(true);
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
        //make sure dialogue only continues after entire dialogue line was shown
        if(canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed(); //if using the unity action script thing
            ContinueStory();
        }
        
    }

    //typing effect
    private IEnumerator DisplayLine(string line)
    {
        //set text to full line, but visible characters to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false); //show player that dialogue is not finished yet, cant continue to next line
        HideChoices();
        canContinueToNextLine = false; //bool false when first displaying line

        foreach(char letter in line.ToCharArray())
        {
            //if submit button pressed, finish displaying line
            if(InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }
            PlayDialogueSound(dialogueText.maxVisibleCharacters);
            dialogueText.maxVisibleCharacters++;
            
            yield return new WaitForSeconds(typingSpeed);
        }

        continueIcon.SetActive(true); //show player that dialogue is finished, can continue to next line
        //display choices if any
        DisplayChoices();
        canContinueToNextLine = true; //bool true once full dialogue line was shown, so cant continue dialogue until finished
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        AudioClip dialogueTypingSoundClip = currentAudioInfo.dialogueTypingSoundClip;
        int frequencyLevel = currentAudioInfo.frequencyLevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;

        //play audio sound every x characters
        if(currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if(stopAudioSource) //in case dialogue sfx is too long, shorten
            {
                audioSource.Stop();
            }
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(dialogueTypingSoundClip); //play text dialogue sound
        }
    }

    private void HideChoices()
    {
        foreach(GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    //reference globals dictionary to set values for variable 
    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("ink variable was null: " + variableName);
        }
        return variableValue;
    }

    /*
    //called when application exits, saves variable states
    public void OnApplicationQuit()
    {
        //if(dialogueVariables != null) 
        dialogueVariables.SaveVariables();
    }
    */
}
