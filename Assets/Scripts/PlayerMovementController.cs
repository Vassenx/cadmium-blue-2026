using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField] private float cameraSpeed = 5f;

    [Header("Unity Game Objects in the Player GO")]
    [SerializeField] private CharacterController characterController;

    [Header("References of other Game Objects")] [SerializeField]
    private CinemachineCamera playerCam;
    private InputHandler inputHandler;

    [SerializeField] private Vector3 currMovement;
    [SerializeField] private float vertLimit;

    public bool movementEnabled = true;

    void Start()
    {
        //see noi evil see no mousey comrade
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movementEnabled = true;

        InputHandler.Instance.Enable();
    }

    private void Update()
    {
        if (movementEnabled)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(InputHandler.Instance.movementVector2.x, 0f, InputHandler.Instance.movementVector2.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    void HandleMovement()
    {
        if (InputHandler.Instance == null)
        { return; }
        
        /*
        currMovement = new Vector3(InputHandler.Instance.movementVector2.x, 0, InputHandler.Instance.movementVector2.y);
        if (!currMovement.AlmostZero())
        {
            currMovement.Normalize();
            characterController.Move(currMovement * Time.deltaTime * speed);
        }
        */
        
        
        Vector3 worldDirection = CalculateWorldDirection();
        currMovement.x = worldDirection.x * speed;
        currMovement.z = worldDirection.z * speed;
        
        if (!currMovement.AlmostZero())
        {
            currMovement.Normalize();
            characterController.Move(currMovement * Time.deltaTime * speed);
        }
    }

    void HandleRotation()
    {
        float mouseX = InputHandler.Instance.cameraMovementVector2.x * cameraSpeed * Time.deltaTime;
        float mouseY = InputHandler.Instance.cameraMovementVector2.y * cameraSpeed * Time.deltaTime;
        
        //camYaw = ClampAngle(camYaw, float.MinValue, float.MaxValue);
        //camPitch = ClampAngle(camPitch, this.minAngle, this.maxAngle);
        
        vertLimit = Mathf.Clamp(vertLimit - mouseY, -30, 60);
        transform.Rotate(0, mouseX, 0);
        playerCam.transform.localRotation = Quaternion.Euler(vertLimit, 0, 0);
        
    }
}
