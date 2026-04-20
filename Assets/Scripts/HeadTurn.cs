using UnityEngine;

public class HeadTurn : MonoBehaviour
{
    public GameObject Head;

    public void TurnHead(Transform turnLocation)
    {
        Vector3 headRotation = turnLocation.position - Head.transform.position;
        Head.transform.rotation  = Quaternion.LookRotation(headRotation, Vector3.up);
    }

}
