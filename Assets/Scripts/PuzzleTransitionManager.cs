using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PuzzleTransitionManager : MonoBehaviour
{
    [Header("Win Condition")] [SerializeField]
    //the puzzle script should enable this and call endpuzzlefunction
    private bool isCompleted = false;
    
    [SerializeField] private List<GameObject> shrines;
    [SerializeField] private CinemachineClearShot puzzleCam;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject player;

    [SerializeField] private RotatePiece wolf;
    [SerializeField] private RotatePiece sheep;


    private void Awake()
    {
        //we necessarily don't need since we can deactivate the script at start in the editor, but we should ensure on awake
        DisableWolfAndSheep();
    }

    public void TriggerPuzzleTransition()
    {
        SwitchToPuzzleCam();
    }

    public void EndPuzzleTransition()
    {
        isCompleted = true;
        DisableWolfAndSheep();
        switchToPlayerCam();
    }

    void SwitchToPuzzleCam()
    {
        player.GetComponent<PlayerMovementController>().movementEnabled = false;
        playerCam.SetActive(false);
        puzzleCam.gameObject.SetActive(true);
        StartCoroutine(EnforceSleep(2f));
        EnableWolfAndSheep();

    }

    
    //hacky and bad practice but for jam will do
    IEnumerator EnforceSleep(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    void switchToPlayerCam()
    {
        playerCam.SetActive(true);
        puzzleCam.gameObject.SetActive(false);
        StartCoroutine(EnforceSleep(2f));
        player.GetComponent<PlayerMovementController>().movementEnabled = true;
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
