using System.Collections;
using UnityEngine;

public class WolfFigLunge : MonoBehaviour
{
    public GameObject target;

    public void Lunge()
    {
        StartCoroutine(AnimateLunge());
    }

    IEnumerator AnimateLunge()
    {
        int timer = 5;
        Vector3 goal = target.transform.position + new Vector3(0, 0.1f, 0);
        while (timer > 0)
        {
            gameObject.transform.LookAt(target.transform);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goal, Vector3.Distance(gameObject.transform.position, goal) / 5.5f);
            timer--;
            yield return new WaitForSeconds(.05f);
        }
        yield return null;
    }
}
