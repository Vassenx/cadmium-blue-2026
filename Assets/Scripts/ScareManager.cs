using System.Collections.Generic;
using UnityEngine;

public class ScareManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> scareObjects;
    private static ScareManager source = null;

    public static ScareManager Main
    {
        get
        {
            if (source == null)
                source = new ScareManager();

            return source;
        }
    }

    public void TriggerScare()
    {
        scareObjects[0].SetActive(true);
        
    }
    
}
