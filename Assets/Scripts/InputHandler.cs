using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class InputHandler : MonoBehaviour
{


    //class handles inputs from input system events and passes it to the controller.
    public Vector2 movementVector2 { get; private set; }
    public Vector2 cameraMovementVector2 { get; private set; }
    
    //Input systems crap
    [SerializeField] private InputActionAsset playerControls;
    //This is the map we are referencing
    [SerializeField] private string actionMapName = "Player";
    //action names
    [SerializeField] private string playerMovementInput = "Move";

    private InputAction playerMovementAction;
    
    public static InputHandler Instance { get; private set; }

    private void Awake()
    {
#region Singleton
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } 
        else {
            Instance = this;
        }
#endregion
    
        InputActionMap playerInputMapReference = playerControls.FindActionMap(actionMapName);
        playerMovementAction = playerInputMapReference.FindAction(playerMovementInput);

        InputEventSub();
    }

    void InputEventSub()
    {
        //need to subscribe to events coming from that InputManager. InputSystems broadcasts the events
        playerMovementAction.performed += inputEvent => movementVector2 = inputEvent.ReadValue<Vector2>();
        playerMovementAction.canceled += inputEvent => movementVector2 = Vector2.zero;
    }

    public void Enable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    public void Disable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}
