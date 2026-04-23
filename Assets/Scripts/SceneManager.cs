using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private float fadeDuration;

    void Update()
    {
        /*
        if (InputHandler.Instance.DebugSpacePressed())
        {
            LoadScene("SamTest");
        }
        */
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneName));
    }

    public void LoadNextScene()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(sceneIndex).name;
        LoadScene(sceneName);
    }
    
    IEnumerator ChangeSceneCoroutine(String sceneName)
    {
        fadeAnimator.SetTrigger("FadeTrigger");
        
        yield return new WaitForSeconds(fadeDuration);

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
