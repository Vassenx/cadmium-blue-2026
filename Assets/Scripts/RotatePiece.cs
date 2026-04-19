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
    Vector3 circleOrigin;
    public bool leftControls = true;
    float timer = 5;
    bool resetting = false;
    public int[] targetZones;
    public RotatePiece pairedPiece;
    public string completeAction;
    public bool isOuter = false;
    public bool isComplete = false;

    //Disaster jank
    public GameObject settingsMenu = null;
    public WolfFigLunge lunge = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pieceTransform = gameObject.GetComponent<Transform>();
        if (leftControls)
            puzzleAction = InputSystem.actions.FindAction("PuzzleLeft");
        else puzzleAction = InputSystem.actions.FindAction("PuzzleRight");
        circleOrigin = circle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOuter && isComplete && completeAction != "STARTSCENE") return;
        if (puzzleAction.WasPerformedThisFrame())
        {
            timer = 5; // Reset timer
            resetting = false;
            float neg = puzzleAction.ReadValue<float>();
            positionChange += (int)neg;
            if (Math.Abs(positionChange) == 16) positionChange = 0;
            
            
            Vector3 dir = transform.position - circleOrigin;
            Quaternion rot = Quaternion.AngleAxis(neg * 22.5f, Vector3.up);
            dir = rot * dir;
            pieceTransform.transform.position = circleOrigin + dir;
            pieceTransform.rotation = rot * transform.rotation;
            
            
            //pieceTransform.RotateAround(circleOrigin, Vector3.up, 22.5f * neg);

            if (((positionChange > 0 && targetZones.Contains(positionChange))
            || (positionChange < 0 && targetZones.Contains(16 - Math.Abs(positionChange))))
            && !isComplete)
            {
                isComplete = true;
                if (isOuter)
                {
                    pairedPiece.enabled = true;
                    pairedPiece.Rise();
                }
            }

            if (pairedPiece.isComplete && isComplete)
            {
                if (completeAction == "STARTSCENE")
                {
                    if (targetZones.Length > 2
                    && ((pairedPiece.positionChange > 0 && (pairedPiece.positionChange == pairedPiece.targetZones[0])) || (pairedPiece.positionChange < 0 && (16 - Math.Abs(pairedPiece.positionChange)) == pairedPiece.targetZones[0])))
                    {
                        if ((positionChange > 0 && positionChange == targetZones[2]) || (positionChange < 0 && (16 - Math.Abs(positionChange)) == targetZones[2]))
                        {
                            Application.Quit();
                        }
                        else if ((positionChange > 0 && positionChange == targetZones[1]) || (positionChange < 0 && (16 - Math.Abs(positionChange)) == targetZones[1]))
                        {
                            settingsMenu.SetActive(true);
                            pairedPiece.enabled = false;
                            gameObject.GetComponent<RotatePiece>().enabled = false;
                        }
                        else if ((positionChange > 0 && positionChange == targetZones[0]) || (positionChange < 0 && (16 - Math.Abs(positionChange)) == targetZones[0]))
                        {
                            // TODO: Swap this out for our custom SceneManager if desired
                            UnityEngine.SceneManagement.SceneManager.LoadScene("ChurchScene");
                        }
                    }
                    else if (pairedPiece.targetZones.Length > 2
                    && ((positionChange > 0 && positionChange == targetZones[0]) || (positionChange < 0 && (16 - Math.Abs(positionChange)) == targetZones[0])))
                    {
                        if ((pairedPiece.positionChange > 0 && pairedPiece.positionChange == pairedPiece.targetZones[2]) || (pairedPiece.positionChange < 0 && (16 - Math.Abs(pairedPiece.positionChange)) == pairedPiece.targetZones[2]))
                        {
                            Application.Quit();
                        }
                        else if ((pairedPiece.positionChange > 0 && pairedPiece.positionChange == pairedPiece.targetZones[1]) || (pairedPiece.positionChange < 0 && (16 - Math.Abs(pairedPiece.positionChange)) == pairedPiece.targetZones[1]))
                        {
                            settingsMenu.SetActive(true);
                            pairedPiece.enabled = false;
                            gameObject.GetComponent<RotatePiece>().enabled = false;
                        }
                        else if ((pairedPiece.positionChange > 0 && (pairedPiece.positionChange == pairedPiece.targetZones[0])) || (pairedPiece.positionChange < 0 && (16 - Math.Abs(pairedPiece.positionChange)) == pairedPiece.targetZones[0]))
                        {
                            UnityEngine.SceneManagement.SceneManager.LoadScene("ChurchScene");
                        }
                    }
                }
                if (completeAction == "PUZZLE1")
                {
                    lunge.Lunge();
                    pairedPiece.enabled = false;
                    gameObject.GetComponent<RotatePiece>().enabled = false;
                    PuzzleTransitionManager ptm = gameObject.transform.parent.transform.parent.parent.GetComponent<PuzzleTransitionManager>();
                    ptm.EndPuzzleTransition();
                    ptm.isCompleted = true;
                }
                if (completeAction == "PUZZLE2")
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    pairedPiece.enabled = false;
                    gameObject.GetComponent<RotatePiece>().enabled = false;
                    PuzzleTransitionManager ptm = gameObject.transform.parent.transform.parent.parent.GetComponent<PuzzleTransitionManager>();
                    ptm.EndPuzzleTransition();
                    ptm.isCompleted = true;
                }
                if (completeAction == "PUZZLE3")
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    pairedPiece.enabled = false;
                    gameObject.GetComponent<RotatePiece>().enabled = false;
                    PuzzleTransitionManager ptm = gameObject.transform.parent.transform.parent.parent.GetComponent<PuzzleTransitionManager>();
                    ptm.EndPuzzleTransition();
                    ptm.isCompleted = true;
                }
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

    public void Rise()
    {
        StartCoroutine(VerticalMove());
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
            //pieceTransform.RotateAround(circleOrigin, Vector3.up, -22.5f);
            Vector3 dir = transform.position - circleOrigin;
            Quaternion rot = Quaternion.AngleAxis(-22.5f, Vector3.up);
            dir = rot * dir;
            pieceTransform.transform.position = circleOrigin + dir;
            pieceTransform.rotation = rot * transform.rotation;
            
            positionChange--;
            yield return new WaitForSeconds(.75f);
        }
        while (positionChange < 0 && resetting)
        {
            //pieceTransform.RotateAround(circleOrigin, Vector3.up, 22.5f);
            
            Vector3 dir = transform.position - circleOrigin;
            Quaternion rot = Quaternion.AngleAxis(22.5f, Vector3.up);
            dir = rot * dir;
            pieceTransform.transform.position = circleOrigin + dir;
            pieceTransform.rotation = rot * transform.rotation;
            
            
            positionChange++;
            yield return new WaitForSeconds(.75f);
        }
        resetting = false;
        yield return null;
    }

    IEnumerator VerticalMove()
    {
        int verticalTimer = 50;
        while (verticalTimer > 0)
        {
            timer = 5;
            float scaling = completeAction == "STARTSCENE" ? 1 : 0.02f;
            gameObject.transform.parent.transform.position += new Vector3(0, 0.25f * scaling, 0);
            verticalTimer--;
            yield return new WaitForSeconds(.05f);
        }
        yield return null;
    }
}
