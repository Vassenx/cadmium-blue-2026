using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class FinalCutsceneScripting : MonoBehaviour
{
    public PuzzleTransitionManager puzzle1;
    public PuzzleTransitionManager puzzle2;
    public PuzzleTransitionManager puzzle3;
    public GameObject spear1;
    public GameObject spear2;
    public GameObject spear3;
    public GameObject wolfPastor;
    public GameObject altar;
    public GameObject flock;
    public PlayerMovementController pmc;
    bool started = false;

    public CinemachineClearShot pastorCam;
    public GameObject playerCam;

    public HeadTurn[] heads;

    public UnityEvent CutSceneStarted;
    
    private void Start()
    {
        heads = new HeadTurn[flock.transform.childCount];

        for(int flockNum = 0; flockNum < flock.transform.childCount; flockNum++)
        {
            heads[flockNum] = flock.transform.GetChild(flockNum).GetComponent<HeadTurn>();
        }
    }


    void OnTriggerEnter(Collider other)
    {
            if(other.gameObject.tag == "Player")
        {
            if (!puzzle1.isCompleted || !puzzle2.isCompleted || !puzzle3.isCompleted || started) return;
            started = true;
            pmc.movementEnabled = false;
            PlayCutscene();   
        }
    }

    public void PlayCutscene()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        CutSceneStarted.Invoke();

        // hide them so we don't see anything in the background when looking at the pastor
        puzzle1.gameObject.SetActive(false);
        puzzle2.gameObject.SetActive(false);
        puzzle3.gameObject.SetActive(false);
        
        altar.GetComponent<AudioSource>().Play();
        SpearController sp1c = spear1.GetComponent<SpearController>();
        SpearController sp2c = spear2.GetComponent<SpearController>();
        SpearController sp3c = spear3.GetComponent<SpearController>();
        sp1c.LiftSpear(5);
        sp2c.LiftSpear(5);
        sp3c.LiftSpear(120);
        yield return new WaitForSeconds(10);
        spear2.GetComponent<AudioSource>().Play();
        sp1c.LowerSpear(5);
        sp2c.LowerSpear(5);
        sp3c.LowerSpear(5);
        yield return new WaitForSeconds(1f);
        wolfPastor.SetActive(true);
        wolfPastor.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds (3);
        flock.SetActive(true);
        yield return new WaitForSeconds(5);
        playerCam.SetActive(false);
        pastorCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(44);
        // JUMPSCARE WITH STING
        foreach(HeadTurn headTurning in heads)
        {
            headTurning.TurnHead(pastorCam.transform);
        }

        yield return new WaitForSeconds(2);
        // PAN UP TO DARK CEILING
        // FADE TO BLACK
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
        yield return null;
    }
}
