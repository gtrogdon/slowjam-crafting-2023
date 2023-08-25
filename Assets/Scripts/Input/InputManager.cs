using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public Actor actor;

    private Action<float> moveHandler;
    private Action interactHandler;
    private Action inventoryHandler;
    private Action submitHandler;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Connect input event handlers with target actor
        moveHandler = InputCommands.moveHorizontalCommand(actor); 
        inventoryHandler = InputCommands.toggleUICommand();
        submitHandler = InputCommands.nextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
       handleInput(); 
    }

    void handleInput() {
        float velocityX = Input.GetAxis("Horizontal");
        moveHandler(velocityX);
        if (Input.GetButtonDown("Inventory")) inventoryHandler();
        if (Input.GetButtonDown("Submit")) submitHandler();
        if (Input.GetButtonDown("Interact") && interactHandler != null) interactHandler(); 
    }

    public static void SetInteractable(Interactable interactableObject) {
        Instance.interactHandler = InputCommands.objectInteractCommand(interactableObject);        
    }

    public static void ClearInteractable() {
        Instance.interactHandler = null;
    }
}
