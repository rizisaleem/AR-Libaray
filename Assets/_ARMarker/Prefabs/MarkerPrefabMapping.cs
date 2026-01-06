using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MarkerActionPair
{
    public string markerName;
    public UnityEvent onDetected; // Assign functions in the inspector
}

