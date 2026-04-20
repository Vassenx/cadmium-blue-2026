using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

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
            //if (!puzzle1.isCompleted || !puzzle2.isCompleted || !puzzle3.isCompleted || started) return;
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
        altar.GetComponent<AudioSource>().Play();
        SpearController sp1c = spear1.GetComponent<SpearController>();
        SpearController sp2c = spear2.GetComponent<SpearController>();
        SpearController sp3c = spear3.GetComponent<SpearController>();
        sp1c.LiftSpear(1);
        sp2c.LiftSpear(1);
        sp3c.LiftSpear(120);
        yield return new WaitForSeconds(15);
        spear2.GetComponent<AudioSource>().Play();
        sp1c.LowerSpear(5);
        sp2c.LowerSpear(5);
        sp3c.LowerSpear(5);
        yield return new WaitForSeconds(1);
        wolfPastor.SetActive(true);
        wolfPastor.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds (5);
        flock.SetActive(true);
        yield return new WaitForSeconds(5);
        playerCam.SetActive(false);
        pastorCam.gameObject.SetActive(true);

        Debug.Log("testpastor cam");
        yield return new WaitForSeconds(14);
        // JUMPSCARE WITH STING
        foreach(HeadTurn headTurning in heads)
        {
            headTurning.TurnHead(pmc.transform);
        }

        yield return new WaitForSeconds(2);
        // PAN UP TO DARK CEILING
        // FADE TO BLACK
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
        yield return null;
    }
}
