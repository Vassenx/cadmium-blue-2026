using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject debugObject;

    [SerializeField]
    private float maxAimDistance = 5f;

    public GameObject currentPuzzle = null;
    
    [SerializeField] LayerMask interactableLayer;
    //just for debugging
    private Vector3 gizmosVector;

    public bool isInteracting = false;
    
    
    //Action is E
    private InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") && other.GetComponent<PuzzleTransitionManager>().isCompleted == false)
        {
            currentPuzzle = other.gameObject;
            other.gameObject.GetComponent<PuzzleTransitionManager>().TriggerPuzzleTransition();
        }
    }

    void Update()
    {

        
    }

    private void FixedUpdate()
    {
        /*
        if (gameObject.GetComponent<PlayerMovementController>().movementEnabled && interactAction.IsPressed())
        {
            TriggerRayCast();   
        }
        */
    }

    void TriggerRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, maxAimDistance, interactableLayer))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                    if (hit.transform.gameObject.TryGetComponent(out PuzzleTransitionManager transitionManager))
                    {
                        if (!transitionManager.isCompleted)
                        {
                            currentPuzzle = hit.transform.gameObject;
                            transitionManager.TriggerPuzzleTransition();   
                        }
                    }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1);
        Gizmos.DrawLine(playerCam.transform.position, new Vector3(gizmosVector.x, gizmosVector.y, gizmosVector.z));
    }
    


}
