using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePuzzleController : MonoBehaviour
{
    [SerializeField] private GameObject sheepDiskObject;
    [SerializeField] private GameObject WolfDiskObject;

    [SerializeField] private GameObject wolf;
    [SerializeField] private GameObject sheep;
    
    [SerializeField] bool leftControls = true;
    
    InputAction puzzleAction;

    private void Start()
    {
        if (leftControls)
        {
            puzzleAction = InputSystem.actions.FindAction("PuzzleLeft");
        }
        else
        {
            puzzleAction = InputSystem.actions.FindAction("PuzzleRight");
        }
    }

    private void Update()
    {
        
    }

    private void CheckForInputs()
    {
        if (puzzleAction.WasPerformedThisFrame())
        {
            
        }
        
    }
}
