using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePiece : MonoBehaviour
{
    Transform pieceTransform;
    InputAction puzzleAction;
    int originalPosition = 0;
    public Transform circle;
    Vector2 circleOrigin;
    public bool leftControls = true;
    float timer = 5;
    bool resetting = false;


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
            originalPosition += (int)neg;
            pieceTransform.RotateAround(circleOrigin, Vector3.up, 22.5f * neg);
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
        while (originalPosition > 0)
        {
            pieceTransform.RotateAround(circleOrigin, Vector3.up, -22.5f);
            originalPosition--;
            yield return new WaitForSeconds(.75f);
        }
        while (originalPosition < 0)
        {
            pieceTransform.RotateAround(circleOrigin, Vector3.up, 22.5f);
            originalPosition++;
            yield return new WaitForSeconds(.75f);
        }
        resetting = false;
        yield return null;
    }
}
