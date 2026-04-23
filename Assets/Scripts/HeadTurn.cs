using System.Collections;
using UnityEngine;

public class HeadTurn : MonoBehaviour
{
    public GameObject Head;
    public Vector3 targetLocation;
    public float rotate;
    public bool debL;

    public void TurnHead(Transform turnLocation)
    {
        Vector3 headRotation = turnLocation.position - Head.transform.position;
        //Vector3 headRotation = Head.transform.position - turnLocation.position;

        if (debL)
        {
            Debug.Log(headRotation);
        }

        rotate = Mathf.Atan(headRotation.z / headRotation.x)*180 / (2 * Mathf.PI);

        rotate += 180;
        //rotate += Mathf.PI;

        //StartCoroutine(EngageTurn(rotate));
        StartCoroutine(EngageAngleTurn(headRotation));

        //Head.transform.rotation = Quaternion.LookRotation(headRotation, Vector3.up);

    }

    IEnumerator EngageAngleTurn(Vector3 headRotation)
    {
        Vector3 currentLook = new Vector3(headRotation.x, headRotation.y, headRotation.z);

        for (int i = 50; i > 0; i--)
        {
            currentLook = new Vector3(headRotation.x, headRotation.y, headRotation.z + ((i - 1) * 2f));

            if (debL)
            {
                Debug.Log(currentLook);
            }

            currentLook.Normalize();
            Head.transform.rotation = Quaternion.LookRotation(currentLook, Vector3.up);
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    IEnumerator EngageTurn(float lookRot)
    {
        float rotValue = 0;
        if (debL)
        {
            Debug.Log(lookRot);
        }

        for (int i = 3065; i > 0; i--)
        {
            if (debL)
            {
                Debug.Log("rotating to : " + rotValue + " || head currently at : " + Head.transform.rotation.y);
            }

            rotValue = (Head.transform.rotation.y + lookRot) / 2;

            if (debL)
            {
                Debug.Log("set Rotate to : " + rotValue);
            }


            //Head.transform.localEulerAngles = new Vector3(0, rotValue, 0);

            if (debL)
            {
                Debug.Log("final is : " + Head.transform.rotation.y);
            }

            //Rotate(new Vector3(0.8f, 0.2f, -0.5f));
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }
}