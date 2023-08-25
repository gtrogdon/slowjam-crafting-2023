using System;
using UnityEngine;

/* This class defines all possible input handlers that the input manager 
can set up with */
public static class InputCommands
{
    public static Action<float> moveHorizontalCommand(Actor gameActor)
    {
        return delegate(float x)
        {
            gameActor.MoveHorizontal(new Vector3(x, 0, 0)); 
        };
    }
    public static Action objectInteractCommand(Interactable interactableObject)
    {
        return delegate()
        {
            interactableObject.OnInteract(); 
        };
    }
    public static Action toggleUICommand()
    {
        return delegate()
        {
            InventoryManager.Instance.toggleInventoryUI();
        };
    }
    public static Action nextDialogue()
    {
        return delegate ()
        {
            DialogueManager.Instance.NextDialogue();
        };
    }
}