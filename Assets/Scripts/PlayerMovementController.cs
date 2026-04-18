using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [Header("Look Params")] [SerializeField]
    private float sensitivity = 0.5f;
    private float vertLimit = 90f;
    
    [Header("Unity Game Objects in the Player GO")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CinemachineCamera cam;

    private Vector3 currMovement;

    private float cameraVertRotation;
    
    void Start()
    {
        //see noi evil see no mousey comrade
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputHandler.Instance.Enable();
    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (InputHandler.Instance == null)
        { return; }
        
        currMovement = new Vector3(InputHandler.Instance.movementVector2.x, 0, InputHandler.Instance.movementVector2.y);
        if (!currMovement.AlmostZero())
        {
            currMovement.Normalize();
            characterController.Move(currMovement * Time.deltaTime * speed);
        }
    }

    void HandleCameraRotation()
    {
        float mouseX = 0f;
        float mouseY = 0f;
        
        //left right
        transform.Rotate(0f, mouseX, 0f);
        //up down
        cameraVertRotation = Mathf.Clamp(cameraVertRotation - mouseY, -vertLimit, vertLimit);
        cam.transform.localRotation = Quaternion.Euler(cameraVertRotation, 0f, 0f);


        //TODO add input mapping for cam and assign to mouseX and MouseY
    }
}
