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
        TriggerRayCast();
    }

    void TriggerRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, Vector3.forward, out hit, maxAimDistance, interactableLayer))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                debugObject.SetActive(true);
                if(interactAction.IsPressed())
                {
                    Debug.Log("Interact");
                    ScareManager scareManager = FindObjectOfType<ScareManager>();
                    scareManager.TriggerScare();
                }
            }
        }
        else
        {
            debugObject.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1);
        Gizmos.DrawLine(playerCam.transform.position, new Vector3(gizmosVector.x, gizmosVector.y, gizmosVector.z));
    }
    


}
