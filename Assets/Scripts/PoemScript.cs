using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemScript : MonoBehaviour
{
    [SerializeField] private GameObject poemObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poemObject.SetActive(true);

        StartCoroutine(OnEsacpe());
        //InputHandler.Instance.EscapePlease.AddListener(OnEsacpe);
    }

    IEnumerator OnEsacpe()
    {
        
        yield return new WaitForSeconds(20f);
        
        poemObject.SetActive(false);

    }
}
