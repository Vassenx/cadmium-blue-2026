using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemScript : MonoBehaviour
{
    [SerializeField] private GameObject poemObject;
    [SerializeField] private PlayerMovementController controller;
    [SerializeField] private AudioSource openingDialogue;

    private bool hasHiddenPoem = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poemObject.SetActive(true);
        controller =  GetComponent<PlayerMovementController>();
        
        StartCoroutine(OnEscape());
    }

    IEnumerator OnEscape()
    {
        yield return new WaitForSeconds(28f);
        
        DisablePoemAndUI();
    }

    public void OnCloseButton()
    {
        DisablePoemAndUI();
    }

    private void DisablePoemAndUI()
    {
        if (hasHiddenPoem)
        { return; }
        
        hasHiddenPoem = true;
        controller.HideMouseCursor();
        poemObject.SetActive(false);
        controller.movementEnabled = true;
        controller.ShowMovementCanvas();
        openingDialogue.Stop();
    }
}
