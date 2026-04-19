using UnityEngine;

[CreateAssetMenu(fileName = "Shrine", menuName = "Scriptable Objects/Shrine")]
public class Shrine : ScriptableObject
{
    [SerializeField] private int shrineOrder;
    [SerializeField] private string shrineLabels;
    [SerializeField] private float beastFrequency;

}
