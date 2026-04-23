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
    public GameObject lookAtPosition;

    public CinemachineClearShot pastorCam;
    public GameObject playerCam;

    public HeadTurn[] heads;
    public GameObject candle;

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
            if(other.gameObject.CompareTag("Player"))
        {
            //if (!puzzle1.isCompleted || !puzzle2.isCompleted || !puzzle3.isCompleted || started) return;
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
        sp3c.LiftSpear(100);
        yield return new WaitForSeconds(10.3f); // slightly after last spear finishes lifting just in case
        spear2.GetComponent<AudioSource>().Play();
        
        sp1c.StopSpear();
        sp2c.StopSpear();
        sp3c.StopSpear();
        
        sp1c.LowerSpear(5);
        sp2c.LowerSpear(5);
        sp3c.LowerSpear(5);
        candle.SetActive(false);
        yield return new WaitForSeconds(4);
        wolfPastor.SetActive(true);
        wolfPastor.GetComponent<AudioSource>().Play();
        flock.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        pastorCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(10);

        playerCam.transform.LookAt(wolfPastor.transform);
        playerCam.transform.position += new Vector3(0, 0.5f, 4);
        yield return new WaitForSeconds(10);
        pastorCam.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        foreach(HeadTurn headTurning in heads)
        {
            headTurning.TurnHead(lookAtPosition.transform);
        }

        yield return new WaitForSeconds(4);
        // TODO: Scrap this and change to rotate slowly upwards into black gradient, transitioning into credits
        /* int timer = 100;
        while (timer > 0)
        {
            playerCam.transform.position += new Vector3(0, 0.2f, 0);
            timer--;
            yield return new WaitForSeconds(0.05f);
        } */
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
        yield return null;
    }
}
