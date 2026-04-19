using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BeastController : MonoBehaviour
{
    [SerializeField] private ScareManager scareManager;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private PlayerMovementController player;
    [SerializeField] private PlayerActionController playerAction;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private CandleSystem playerCandle;
    [SerializeField] private AudioSource audioSource;
    public bool beastIsActive = false;

    public float timer = 18f;

    private float tracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tracker = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(beastIsActive)
        {
            //growl and sniflles last 18 seconds
            timer -= Time.deltaTime;
            if (timer <= 5f)
            {
                CheckIfPlayerIsNotHiding();
            }
            if (timer <= 0f)
            {
                scareManager.beastTimer = scareManager.beastTracker;
                beastIsActive = false;
                timer = 18f;
                gameObject.SetActive(false);
            
            }   
        }
        
    }


    void CheckIfPlayerIsNotHiding()
    {
        if (player.isInPuzzle && playerCandle.isCandleOn)
        {
            scareManager.beastTimer = scareManager.beastTracker;
            timer = 18f;
            playerObject.transform.position = spawnPoint.transform.position;
            beastIsActive = false;
            audioSource.Stop();
            scareManager.ResetBeastTimer();
            playerAction.currentPuzzle.GetComponent<PuzzleTransitionManager>().EndPuzzleTransition();
            gameObject.SetActive(false);
        }
    }
    
    IEnumerator EnforceSleep(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    
}
