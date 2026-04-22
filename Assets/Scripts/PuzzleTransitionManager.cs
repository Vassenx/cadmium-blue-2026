using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleTransitionManager : MonoBehaviour
{
    [Header("Win Condition")] [SerializeField]
    //the puzzle script should enable this and call endpuzzlefunction
    public bool isCompleted = false;

    public UnityEvent<bool> OnPuzzleTransition; // true = switch to puzzle, false = switch back to main
    
    [SerializeField] private List<GameObject> shrines;
    [SerializeField] private CinemachineClearShot puzzleCam;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject player;

    [SerializeField] private RotatePiece wolf;
    [SerializeField] private RotatePiece sheep;

    [SerializeField] private SpriteRenderer poemFront;
    [SerializeField] private SpriteRenderer poemBack;

    [SerializeField] private Canvas puzzleInputCanvas;

    private void Awake()
    {
        //we necessarily don't need since we can deactivate the script at start in the editor, but we should ensure on awake
        DisableWolfAndSheep();
        HidePoem();
        HidePuzzleCanvas();
    }

    public void TriggerPuzzleTransition()
    {
        SwitchToPuzzleCam();
        OnPuzzleTransition.Invoke(true);
    }

    public void EndPuzzleTransition()
    {
        isCompleted = true;
        DisableWolfAndSheep();
        HidePuzzleCanvas();
        switchToPlayerCam();
        OnPuzzleTransition.Invoke(false);
    }

    void SwitchToPuzzleCam()
    {
        player.GetComponent<PlayerMovementController>().movementEnabled = false;
        player.GetComponent<PlayerMovementController>().isInPuzzle = true;
        playerCam.GetComponent<CinemachineCamera>().enabled = false;
        puzzleCam.gameObject.SetActive(true);
        StartCoroutine(EnforceSleep(2f));
        EnableWolfAndSheep();
        ShowPoem();
        ShowPuzzleCanvas();
    }
    
    void HidePoem()
    {
        poemFront.enabled = false;
        poemBack.enabled = false;
    }

    void ShowPoem()
    {
        poemFront.enabled = true;
        poemBack.enabled = true;
    }

    void ShowPuzzleCanvas()
    {
        puzzleInputCanvas.gameObject.SetActive(true);
    }

    void HidePuzzleCanvas()
    {
        puzzleInputCanvas.gameObject.SetActive(false);
    }

    //hacky and bad practice but for jam will do
    IEnumerator EnforceSleep(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    void switchToPlayerCam()
    {
        playerCam.GetComponent<CinemachineCamera>().enabled = true;
        puzzleCam.gameObject.SetActive(false);
        StartCoroutine(EnforceSleep(2f));
        player.GetComponent<PlayerMovementController>().movementEnabled = true;
        player.GetComponent<PlayerMovementController>().isInPuzzle = false;
    }

    void EnableWolfAndSheep()
    {
        wolf.enabled = true;
        sheep.enabled = true;
    }
    
    void DisableWolfAndSheep()
    {
        wolf.enabled = false;
        sheep.enabled = false;
    }
    
}
