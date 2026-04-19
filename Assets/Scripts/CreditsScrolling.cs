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

    void Start()
    {
        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        for (int i = 160; i > 0; i--)
        {
            // TODO: Change this to the wolf
            candle.transform.position -= new Vector3(0, 0.08f, 0);
            // Rotates as falling
            candle.transform.Rotate(new Vector3(-0.8f, 0, 0.5f));
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1);
        audioSource.Play();
        // Text and sheep rain scrolling
        for (int i = 1200; i > 0; i--)
        {
            text.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 2);
            sheep.transform.position -= new Vector3(0, 0.06f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        // TODO: candle falling
        // TODO: sheep pile up
        // TODO: title fade in
        yield return null;
    }
}
