using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Actor actor;

    private Action<float> moveHandler;
    private Action jumpHandler;
    private Action inventoryHandler;
    private Action submitHandler;

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
        if(Input.GetButtonDown("Inventory")) inventoryHandler();
        if (Input.GetButtonDown("Submit")) submitHandler();
    }
}
