using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    public void TriggerDialogue()
    {
        Debug.Log("trigger button pressed");
        DialogueManager.Instance.EnterDialogueMode(inkJSON);
    }
}
