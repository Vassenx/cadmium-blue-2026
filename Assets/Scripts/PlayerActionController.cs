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
            Debug.Log("tioitr");
            TriggerRayCast();   
        }
    }

    void TriggerRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, Vector3.forward, out hit, maxAimDistance, interactableLayer))
        {
            Debug.Log("hit");
            if (hit.transform.CompareTag("Interactable"))
            {
                Debug.Log("interact");
                if(interactAction.IsPressed())
                {
                    if (hit.transform.gameObject.TryGetComponent(out PuzzleTransitionManager transitionManager))
                    {
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
