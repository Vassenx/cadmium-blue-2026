using System.Collections;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    public Vector3 shutPosition;
    public Vector3 openPosition;
    public bool open;

    public void LiftSpear()
    {
        StartCoroutine(MoveSpear(1, 10));
    }
    public void LowerSpear()
    {
        StartCoroutine(MoveSpear(-1, 3));
    }

    IEnumerator MoveSpear(int direction, int duration)
    {
        Vector3 goal = direction > 0 ? openPosition : shutPosition;
        Vector3 startingPosition = direction > 0 ? shutPosition : openPosition;
        while (duration > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goal, Vector3.Distance(startingPosition, goal)/duration);
            duration --;
            yield return new WaitForSeconds(duration);
        }
        yield return null;
    }
}
