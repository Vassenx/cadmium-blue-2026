using System.Collections;
using UnityEngine;

public class CreditsScrolling : MonoBehaviour
{
    public GameObject text;
    public GameObject candle;
    public GameObject sheep;
    public GameObject wolf;
    public GameObject sheepPile;
    public GameObject title;

    void Start()
    {
        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        for (int i = 160; i > 0; i--)
        {
            candle.transform.position -= new Vector3(0, 0.08f, 0);
            candle.transform.Rotate(new Vector3(-0.8f, 0, 0.5f));
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1);
        for (int i = 1000; i > 0; i--)
        {
            text.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 2);
            if (sheep.transform.position.y > 24) sheep.transform.position -= new Vector3(0, 0.08f, 0);
            else if (sheep.transform.position.y > 21) sheep.transform.position -= new Vector3(0, 0.04f, 0);
            else if (sheep.transform.position.y > 14) sheep.transform.position -= new Vector3(0, 0.08f, 0);
            else if (sheep.transform.position.y > 11) sheep.transform.position -= new Vector3(0, 0.04f, 0);
            else if (sheep.transform.position.y > 4) sheep.transform.position -= new Vector3(0, 0.08f, 0);
            else if (sheep.transform.position.y > 1) sheep.transform.position -= new Vector3(0, 0.04f, 0);
            else sheep.transform.position -= new Vector3(0, 0.08f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        // sheep down
        // wolf down
        // sheep pile up
        // title down
        yield return null;
    }
}
