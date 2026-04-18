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
    public GameObject settingsMenu = null;

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
            if (Math.Abs(positionChange) == 16) positionChange = 0;
            pieceTransform.RotateAround(circleOrigin, Vector3.up, 22.5f * neg);

            if ((pairedPiece.targetZones.Contains(pairedPiece.positionChange) 
            || pairedPiece.targetZones.Contains(-(16 - Math.Abs(pairedPiece.positionChange)))) 
            && (targetZones.Contains(positionChange)
            || targetZones.Contains(-(16 - Math.Abs(positionChange)))))
            {
                if (((targetZones.Length > 0 && (positionChange == targetZones[0] || (-(16 - Math.Abs(positionChange))) == targetZones[0])) 
                    || pairedPiece.targetZones.Length > 0 && (pairedPiece.positionChange == pairedPiece.targetZones[0]) || (-(16 - Math.Abs(pairedPiece.positionChange))) == pairedPiece.targetZones[0])
                    && completeAction == "STARTSCENE")
                {
                    // TODO: START GAME/GO TO GAME SCENE
                }
                if (targetZones.Length > 1 
                    || pairedPiece.targetZones.Length > 1
                    && completeAction == "STARTSCENE")
                {
                    if ((positionChange == targetZones[1] || (-(16 - Math.Abs(positionChange))) == targetZones[1]) 
                        || ((pairedPiece.positionChange == pairedPiece.targetZones[1]) || (-(16 - Math.Abs(pairedPiece.positionChange))) == pairedPiece.targetZones[1]))
                        settingsMenu.SetActive(true);
                }
                if (targetZones.Length > 2
                    || pairedPiece.targetZones.Length > 2
                    && completeAction == "STARTSCENE")
                {
                    if ((positionChange == targetZones[2] || (-(16 - Math.Abs(positionChange))) == targetZones[2]) 
                        || ((pairedPiece.positionChange == pairedPiece.targetZones[2]) || (-(16 - Math.Abs(pairedPiece.positionChange))) == pairedPiece.targetZones[2]))
                        Application.Quit();
                }
                if (completeAction == "PUZZLE1")
                {
                    // TODO: SET PUZZLE1 IS COMPLETE
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
