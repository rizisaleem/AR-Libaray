using UnityEngine; 
using UnityEngine.XR.ARFoundation; 
using UnityEngine.XR.ARSubsystems; 
using System.Collections.Generic;

public class MarkerActionInvoker : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;
    [SerializeField] MarkerActionPair[] markerActions;

    private Dictionary<string, MenuManager.Menu> markerLookup;

    void Awake()
    {
        markerLookup = new Dictionary<string, MenuManager.Menu>();

        foreach (var pair in markerActions)
        {
            if (!markerLookup.ContainsKey(pair.markerName))
                markerLookup.Add(pair.markerName, pair.menuToEnable);
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
            HandleImage(img);

        foreach (var img in args.updated)
            HandleImage(img);      
    }

    void HandleImage(ARTrackedImage img)
    {
        if (img.trackingState != TrackingState.Tracking)
            return;

        string markerName = img.referenceImage.name;

        if (markerLookup.TryGetValue(markerName, out var menu))
        {
            GameManager.Instance.DisableAR();
            MenuManager.Instance.ChangeMenu(menu);
        }
    }
}
