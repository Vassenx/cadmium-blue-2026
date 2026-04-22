using System.Collections;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    public Vector3 shutPosition;
    public Vector3 openPosition;
    public bool open;

    public void LiftSpear(int duration)
    {
        StartCoroutine(MoveSpear(1, duration));
    }

    public void StopSpear()
    {
        StopAllCoroutines();
    }
    
    public void LowerSpear(int duration)
    {
        StartCoroutine(MoveSpear(-1, duration));
    }

    IEnumerator MoveSpear(int direction, int duration)
    {
        Vector3 goal = direction > 0 ? openPosition : shutPosition;
        Vector3 startingPosition = direction > 0 ? shutPosition : openPosition;
        while (duration > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goal, Vector3.Distance(startingPosition, goal)/duration);
            duration --;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
