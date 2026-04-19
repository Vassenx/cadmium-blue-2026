using System.Collections;
using UnityEngine;

public class FinalCutsceneScripting : MonoBehaviour
{
    public GameObject spear1;
    public GameObject spear2;
    public GameObject spear3;
    public GameObject wolfPastor;
    public GameObject flock;
    // TODO: Audio and music

    void Start()
    {
        // TODO: CHANGE THIS TO RUN AFTER PLAYER HITS COLLIDER ON WALK BACK TO DOOR
        PlayCutscene();
    }

    public void PlayCutscene()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        SpearController sp1c = spear1.GetComponent<SpearController>();
        SpearController sp2c = spear2.GetComponent<SpearController>();
        SpearController sp3c = spear3.GetComponent<SpearController>();
        sp1c.LiftSpear(1);
        sp2c.LiftSpear(1);
        sp3c.LiftSpear(70);
        yield return new WaitForSeconds(8);
        spear2.GetComponent<AudioSource>().Play();
        sp1c.LowerSpear(5);
        sp2c.LowerSpear(5);
        sp3c.LowerSpear(5);
        yield return new WaitForSeconds(3);
        wolfPastor.SetActive(true);
        wolfPastor.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds (2);
        flock.SetActive(true);
        yield return new WaitForSeconds(10);
        // TURN CAMERA
        // PLAY PASTOR DIALOGUE
        // JUMPSCARE WITH STING
        yield return new WaitForSeconds(4);
        // PAN UP TO DARK CEILING
        // FADE TO BLACK
        // CREDITS
        yield return null;
    }
}
