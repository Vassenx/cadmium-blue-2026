using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [Header("Unity Game Objects in the Player GO")]
    [SerializeField] private CharacterController characterController;

    private Vector3 currMovement;

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
}
