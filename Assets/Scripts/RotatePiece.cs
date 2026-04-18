using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePiece : MonoBehaviour
{
    Transform pieceTransform;
    InputAction puzzleAction;
    public int positionChange = 0;
    public Transform circle;
    Vector2 circleOrigin;
    public bool leftControls = true;
    float timer = 5;
    bool resetting = false;
    public int[] targetZones;
    public RotatePiece pairedPiece;
    public string completeAction;

    //Disaster jank
    public GameObject settingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pieceTransform = gameObject.GetComponent<Transform>();
        if (leftControls)
            puzzleAction = InputSystem.actions.FindAction("PuzzleLeft");
        else puzzleAction = InputSystem.actions.FindAction("PuzzleRight");
        circleOrigin = circle.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleAction.WasPerformedThisFrame())
        {
            timer = 5; // Reset timer
            resetting = false;
            float neg = puzzleAction.ReadValue<float>();
            positionChange += (int)neg;
            if (positionChange == 12) positionChange = 0;
            pieceTransform.RotateAround(circleOrigin, Vector3.up, 22.5f * neg);

            if ((pairedPiece.targetZones.Contains(pairedPiece.positionChange) 
            || pairedPiece.targetZones.Contains(-(12 - Math.Abs(pairedPiece.positionChange)))) 
            && (targetZones.Contains(positionChange)
            || targetZones.Contains(-(12 - Math.Abs(positionChange)))))
            {
                if (((targetZones.Length > 1 && positionChange == targetZones[1]) 
                    || pairedPiece.targetZones.Length > 1 && pairedPiece.positionChange == pairedPiece.targetZones[1])
                    && completeAction == "STARTSCENE")
                {
                    settingsMenu.SetActive(true);
                }
                pairedPiece.enabled = false;
                gameObject.GetComponent<RotatePiece>().enabled = false;
            }
        }
        else if (timer <= 0 && !resetting)
        {
            StartCoroutine(Reset());
        }
        else if (!resetting)
        {
            timer -= Time.deltaTime;
        }
    }

    IEnumerator Reset()
    {
        resetting = true;
        if (timer > 0)
        {
            yield return null;
        }
        while (positionChange > 0 && resetting)
        {
            pieceTransform.RotateAround(circleOrigin, Vector3.up, -22.5f);
            positionChange--;
            yield return new WaitForSeconds(.75f);
        }
        while (positionChange < 0 && resetting)
        {
            pieceTransform.RotateAround(circleOrigin, Vector3.up, 22.5f);
            positionChange++;
            yield return new WaitForSeconds(.75f);
        }
        resetting = false;
        yield return null;
    }
}
