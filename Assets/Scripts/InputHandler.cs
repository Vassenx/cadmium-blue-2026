using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Vector2 = UnityEngine.Vector2;

public class InputHandler : MonoBehaviour
{
    //class handles inputs from input system events and passes it to the controller.
    public Vector2 movementVector2 { get; private set; }
    public Vector2 cameraMovementVector2 { get; private set; }
    public bool heldCandleButton { get; private set; }

    public UnityEvent<bool> CandleButtonHeld;
    
    //Input systems crap
    [SerializeField] private InputActionAsset playerControls;
    //This is the map we are referencing
    [SerializeField] private string actionMapName = "Player";
    //action names
    [SerializeField] private string playerMovementInput = "Move";
    [SerializeField] private string spaceDebugInput = "Jump"; // for debugging
    [SerializeField] private string mouseDeltaInput = "MouseDelta";
    [SerializeField] private string candleInput = "Candle";

    private InputAction playerMovementAction;
    private InputAction spaceDebugAction;
    private InputAction mouseDeltaAction;
    private InputAction candleAction;
    
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
        spaceDebugAction = playerInputMapReference.FindAction(spaceDebugInput);
        mouseDeltaAction = playerInputMapReference.FindAction(mouseDeltaInput);
        candleAction = playerInputMapReference.FindAction(candleInput);
        
        InputEventSub();
    }

    void InputEventSub()
    {
        //need to subscribe to events coming from that InputManager. InputSystems broadcasts the events
        playerMovementAction.performed += inputEvent => movementVector2 = inputEvent.ReadValue<Vector2>();
        playerMovementAction.canceled += inputEvent => movementVector2 = Vector2.zero;
        
        //camera couch caaast boom badu boom ba tss
        mouseDeltaAction.performed += inputEvent => cameraMovementVector2 = inputEvent.ReadValue<Vector2>();
        mouseDeltaAction.canceled += inputEvent => cameraMovementVector2 = Vector2.zero;

        candleAction.performed += inputEvent =>
        {
            if (inputEvent.interaction is HoldInteraction)
            {
                CandleButtonHeld.Invoke(true);
            }
        };

        candleAction.canceled += inputEvent => CandleButtonHeld.Invoke(false);
    }
    
    public bool DebugSpacePressed()
    {
        return spaceDebugAction.WasPressedThisFrame();
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
