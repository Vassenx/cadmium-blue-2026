using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemScript : MonoBehaviour
{
    [SerializeField] private GameObject poemObject;
    [SerializeField] private PlayerMovementController controller;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poemObject.SetActive(true);
        controller =  GetComponent<PlayerMovementController>();
        
        StartCoroutine(OnEscape());
    }

    IEnumerator OnEscape()
    {
        yield return new WaitForSeconds(10f);
        
        DisablePoemAndUI();
    }

    public void OnCloseButton()
    {
        DisablePoemAndUI();
    }

    private void DisablePoemAndUI()
    {
        controller.HideMouseCursor();
        poemObject.SetActive(false);
    }
}
