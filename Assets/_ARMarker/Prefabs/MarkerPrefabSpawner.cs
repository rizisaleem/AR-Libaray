using UnityEngine; 
using UnityEngine.XR.ARFoundation; 
using UnityEngine.XR.ARSubsystems; 
using UnityEngine.Events; 
using System.Collections.Generic;

public class MarkerActionInvoker : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;
    [SerializeField] MarkerActionPair[] markerActions;

    Dictionary<string, UnityEvent> actionLookup;

    // 👇 ADD THIS HERE (class-level variable)
    // HashSet<string> triggeredMarkers = new HashSet<string>();

    void Awake()
    {
        actionLookup = new Dictionary<string, UnityEvent>();
        foreach (var pair in markerActions)
        {
            if (pair.onDetected != null)
                actionLookup[pair.markerName] = pair.onDetected;
        }
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImagesChanged;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImagesChanged;
    }

    void OnImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
            InvokeAction(img);
        

        foreach (var img in args.updated)
            InvokeAction(img);
        
    }

    void InvokeAction(ARTrackedImage img)
    {
        if (img.trackingState != TrackingState.Tracking)
            return;

        string name = img.referenceImage.name;

        // 👇 Prevent multiple calls
        // if (triggeredMarkers.Contains(name))
        //     return;

        if (actionLookup.TryGetValue(name, out var action))
        {
            action.Invoke();
            // triggeredMarkers.Add(name); // mark as triggered
        }
    }
}
