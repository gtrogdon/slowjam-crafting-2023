using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    protected virtual void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            InputManager.SetInteractable(this);
        }
    }

    protected virtual void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            InputManager.ClearInteractable();
        }
    }

    public abstract void OnInteract();  // event handler for when user presses 'e'
                                        // in range of the object
}