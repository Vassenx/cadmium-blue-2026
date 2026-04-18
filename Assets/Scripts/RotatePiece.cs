using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePiece : MonoBehaviour
{
    RectTransform rt;
    InputAction puzzleAction;
    public RectTransform circle;
    Vector2 circleOrigin;
    public bool leftControls = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        if (leftControls)
            puzzleAction = InputSystem.actions.FindAction("PuzzleLeft");
        else puzzleAction = InputSystem.actions.FindAction("PuzzleRight");
        circleOrigin = circle.TransformPoint(circle.rect.center);
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleAction.WasPerformedThisFrame())
        {
            float neg = -puzzleAction.ReadValue<float>();
            rt.RotateAround(circleOrigin, Vector3.forward, 22.5f * neg);
        }
    }
}
