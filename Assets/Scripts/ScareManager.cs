using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScareManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    
    [SerializeField]
    List<GameObject> scareObjects;

    [SerializeField] private AudioLibrary ambianceSounds;
    [SerializeField] private AudioLibrary beastSounds;

    [SerializeField] private GameObject player;
    
    private PlayerMovementController playerMovementController;
    private static ScareManager source = null;

    [Header("Parameters for randomaized sounds")] [SerializeField]
    private float randomSoundTimer = 60f;

    private float ambianceTracker;
    
    void Start()
    {
        ambianceTracker = randomSoundTimer;
        playerMovementController = player.GetComponent<PlayerMovementController>();
    }
    
    public static ScareManager Main
    {
        get
        {
            if (source == null)
                source = new ScareManager();

            return source;
        }
    }

    private void Update()
    {
        //Random sounds
        randomSoundTimer -= Time.deltaTime;
        if (randomSoundTimer <= 0.0f)
        {
            RandomSoundsTrigger();
        }
        
        
        //logic to decide when to trigger but at abnormal intervals depending on what the player is doing
        //at random occurrences, trigger beast sounds
        if(playerMovementController.isInPuzzle)
        {
            
        }
    }

    public void TriggerScare()
    {
        //TODO
        scareObjects[0].SetActive(true);
    }

    public void TriggerBeast()
    {
 
    }


    void RandomSoundsTrigger()
    {
        int randIndex = Random.RandomRange(0, ambianceSounds.audioClips.Count);
        audioSource.PlayOneShot(ambianceSounds.audioClips[randIndex]);
        randomSoundTimer = ambianceTracker;
    }
}
