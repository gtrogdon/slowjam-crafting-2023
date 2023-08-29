using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public delegate void OnDialogueTriggered();
    public OnDialogueTriggered OnDialogueTriggeredCallback;

    // Test
    private bool isFoundParts = false;
    private bool isPaperArm = false;
    private bool isPipeArm = false;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text displayNameText;

    /* tried to access from parent but had trouble, 
     * needs to be gameobject to set event system selected game object 
     */
    [Header("Choice UI")]
    [SerializeField] public GameObject[] choices;
    private TMP_Text[] choicesTexts;

    private Story currentStory;
    private bool dialogueIsPlaying;
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private DialogueVariables dialogueVariables;
    private InkExternalFunctions inkExternalFunctions;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        inkExternalFunctions = new InkExternalFunctions();
    }

    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get text for each choice which is a child of each choice's game object
        choicesTexts = new TMP_Text[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesTexts[index] = choice.GetComponentInChildren<TMP_Text>();
            index++;
        }

    }

    public void NextDialogue()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        else
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        InventoryManager.Instance.DialogueChecks(); // LOL merp
        currentStory = new Story(inkJSON.text);
        UpdateStoryVariables();

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory);

        // reset speaker, protrait, layout
        displayNameText.text = "???";
        //portraitAnimator.Play("default");
        //layoutanimator.Play("right");

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string nextLine = currentStory.Continue();
            if (nextLine.Equals("") && !currentStory.canContinue) // if last line external function, avoid showing empty string
            {
                ExitDialogueMode();
                return;
            }
            dialogueText.text = nextLine;
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // make sure UI can support number of choices from ink story
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than UI can support. Number of choices: " + currentChoices.Count);
        }

        // enable and initialize choices UI
        for (int index = 0; index < currentChoices.Count; index++)
        {
            choices[index].gameObject.SetActive(true);
            choicesTexts[index].text = currentChoices[index].text;
        }

        // hide extra choices UI, starting from index = number of provided story choices
        if (choices.Length > currentChoices.Count)
        {
            for (int i = currentChoices.Count; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(false);
            }
        }
        StartCoroutine(SelectFirstChoice());
    }

    /* Is there a better way to do this? - unity event system needs to be clear first then set after at least one frame */
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(":");
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag couldn't be parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displaySpeakerNameByTag(tagValue);
                    break;
                case PORTRAIT_TAG:
                    // portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    // layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag name not handled: " + tag);
                    break;
            }
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.TryGetValue(variableName, variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was not found: " + variableName);
        }
        return variableValue;
    }

    private void displaySpeakerNameByTag(string tagValue)
    {
        if (tagValue.StartsWith("{") && tagValue.EndsWith("}") )
        {
            displayNameText.text = ((Ink.Runtime.StringValue) GetVariableState(tagValue.Substring(1, tagValue.Length - 2))).value;
        } else
        {
            displayNameText.text = tagValue;
        }
    }

    public void SetFoundParts()
    {
        isFoundParts = true;
    }

    public void SetPipeArm()
    {
        isPipeArm = true;
    }

    public void SetPaperArm()
    {
        isPaperArm = true;
    }

    private void UpdateStoryVariables()
    {
        if (isFoundParts)
        {
            Ink.Runtime.Object value = Ink.Runtime.Value.Create(true);
            dialogueVariables.SetVariable("foundParts", value);
        }
        if (isPipeArm)
        {
            Ink.Runtime.Object value = Ink.Runtime.Value.Create(true);
            dialogueVariables.SetVariable("pipeArm", value);
            dialogueVariables.SetVariable("armCrafted", value);
        }
        if (isPaperArm)
        {
            Ink.Runtime.Object value = Ink.Runtime.Value.Create(true);
            dialogueVariables.SetVariable("paperArm", value);
            dialogueVariables.SetVariable("armCrafted", value);
        }
    }
}