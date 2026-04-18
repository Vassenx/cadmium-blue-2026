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
    [SerializeField] private Movement playerMovementInput;


    private Vector3 currMovement;

    private float cameraVertRotation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //see noi evil see no mousey comrade
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    void HandleMovement()
    {
        Vector3 worldDir = CalculateWorldVectorPlayer();
        currMovement.x = worldDir.x * speed;
        currMovement.y = worldDir.y * speed;
        
        characterController.Move(currMovement * Time.deltaTime);
    }

    void HandleRotation()
    {
       //TODO
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

    //helper method 
    Vector3 CalculateWorldVectorPlayer()
    {
        Vector3 inputDir = new Vector3(playerMovementInput.movementVector2.x, 0f,
            playerMovementInput.movementVector2.y);
        
        return transform.TransformDirection(inputDir).normalized;
    }
}
