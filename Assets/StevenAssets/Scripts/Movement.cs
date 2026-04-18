using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class Movement : MonoBehaviour
{
    //class handles inputs from input system events and passes it to the controller.
    public Vector2 movementVector2 { get; private set; }
    public Vector2 cameraMovementVector2 { get; private set; }
    
    //Input systems crap
    [SerializeField] private InputActionAsset playerControls;
    //This is the map we are referencing
    [SerializeField] private string actionMapName = "PlayerMovement";
    //action names
    [SerializeField] private string playerMovementInput = "WASD";

    private InputAction playerMovementAction;


    private void Awake()
    {
        InputActionMap playerInputMapReference = playerControls.FindActionMap(actionMapName);
        playerMovementAction = playerInputMapReference.FindAction(playerMovementInput);
    }

    void InputEventSub()
    {
        //need to subscribe to events coming from that InputManager. InputSystems broadcasts the events
        playerMovementAction.performed += inputEvent => inputEvent.ReadValue<Vector2>();
        playerMovementAction.canceled += inputEvent => movementVector2 = Vector2.zero;
    }


    void Enable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}
