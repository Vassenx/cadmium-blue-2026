using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScareManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    
    [SerializeField]
    List<GameObject> scareObjects;

    [SerializeField] private List<GameObject> audioCues;
    [SerializeField] private List<AudioClip> beastAudioCues;

    [SerializeField] private GameObject player;
    
    private PlayerMovementController playerMovementController;
    private static ScareManager source = null;
    
    void Start()
    {
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
        int randomNum = Random.Range(0, beastAudioCues.Count);
        audioSource.PlayOneShot(beastAudioCues[randomNum]);
    }
    
}
