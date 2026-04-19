using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private PuzzleTransitionManager[] puzzleTransitionManagers;

    [SerializeField] private AudioClip mainMusic;
    [SerializeField] private AudioClip puzzleMusic;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        puzzleTransitionManagers = Object.FindObjectsByType<PuzzleTransitionManager>(FindObjectsSortMode.None);
        foreach (var puzzleTransitionManager in puzzleTransitionManagers)
        {
            puzzleTransitionManager.OnPuzzleTransition.AddListener(OnPuzzleTransition);
        }
    }

    // TODO: IEnumerator slow fade
    private void OnPuzzleTransition(bool toPuzzle)
    {
        AudioClip nextClip = toPuzzle ? puzzleMusic : mainMusic;
        
        audioSource.Stop();
        audioSource.clip = nextClip;
        audioSource.Play();
    }
}
