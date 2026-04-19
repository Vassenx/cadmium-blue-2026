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
        while (timer > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, Vector3.Distance(gameObject.transform.position, target.transform.position) / 5);
            timer--;
            yield return new WaitForSeconds(.05f);
        }
        yield return null;
    }
}
