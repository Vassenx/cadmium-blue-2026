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

    public GameObject currentPuzzle;
    
    [SerializeField] LayerMask interactableLayer;
    //just for debugging
    private Vector3 gizmosVector;
    
    
    //Action is E
    private InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        
    }
    
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<PlayerMovementController>().movementEnabled)
        {
            TriggerRayCast();   
        }
    }

    void TriggerRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, maxAimDistance, interactableLayer))
        {
            Debug.Log("debug");
            if (hit.transform.CompareTag("Interactable"))
            {
                Debug.Log("interact");
                if(interactAction.IsPressed())
                {
                    if (hit.transform.gameObject.TryGetComponent(out PuzzleTransitionManager transitionManager))
                    {
                        transitionManager.TriggerPuzzleTransition();
                        currentPuzzle = hit.transform.gameObject;
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
