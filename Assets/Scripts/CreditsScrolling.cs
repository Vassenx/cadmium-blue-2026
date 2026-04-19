using System.Collections;
using UnityEngine;

public class CreditsScrolling : MonoBehaviour
{
    public GameObject text;
    public GameObject wolf;
    public GameObject sheep;
    public GameObject candle;
    public GameObject sheepPile;
    public GameObject title;
    public AudioSource audioSource;
    public Transform[] allSheep;

    void Start()
    {
        allSheep = new Transform[sheep.transform.childCount];

        for (int sheepCount = 0; sheepCount < sheep.transform.childCount; sheepCount++)
        {
            allSheep[sheepCount] = sheep.transform.GetChild(sheepCount);
        }

        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        for (int i = 165; i > 0; i--)
        {
            // DONE : Changed to wolf
            wolf.transform.position -= new Vector3(0, 0.1f, 0);
            wolf.transform.Rotate(new Vector3(0.8f, 0.2f, -0.5f));
            yield return new WaitForSeconds(0.05f);
        }

        audioSource.Play();
        yield return new WaitForSeconds(2);

        // Text and sheep rain scrolling
        for (int i = 700; i > 0; i--)
        {
            text.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 3.5f);
            sheep.transform.position -= new Vector3(0, 0.06f, 0);
            foreach(Transform individualSheep in allSheep)
            {
                individualSheep.Rotate(new Vector3(0f, 0.2f, -0.5f));
            }
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        for (int i = 175; i > 0; i--)
        {
            candle.transform.position -= new Vector3(0, 0.08f, 0);
            sheepPile.transform.position += new Vector3(0, 0.05f, 0);
            candle.transform.Rotate(new Vector3(0, 0.2f, 0.4f));
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 180; i > 0; i--)
        {
            candle.transform.position += new Vector3(0, 0.05f, 0);
            sheepPile.transform.position += new Vector3(0, 0.05f, 0);
            yield return new WaitForSeconds(0.05f);
        }

        // TODO: title fade in
        yield return null;
    }
}
