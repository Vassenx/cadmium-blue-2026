using System.Collections;
using UnityEngine;

public class HeadTurn : MonoBehaviour
{
    public GameObject Head;
    public Vector3 targetLocation;
    public float rotate;

    public void TurnHead(Transform turnLocation)
    {
        Vector3 headRotation = turnLocation.position - Head.transform.position;

        rotate = Mathf.Atan(headRotation.y / headRotation.x);

        EngageTurn(rotate);

        //Head.transform.rotation = Quaternion.LookRotation(headRotation, Vector3.up);

    }

    IEnumerator EngageTurn(float lookRot)
    {
        float rotValue = 0;

        for (int i = 165; i > 0; i--)
        {
            rotValue = (Head.transform.rotation.y + lookRot) / 2;

            
            Head.transform.localEulerAngles = new Vector3(Head.transform.rotation.x, rotValue, Head.transform.rotation.z);

            //Rotate(new Vector3(0.8f, 0.2f, -0.5f));
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }
}