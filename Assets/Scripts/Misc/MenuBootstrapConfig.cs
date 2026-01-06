using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuBootstrapConfig", menuName = "Config/Menu Bootstrap Config")]
public class MenuBootstrapConfig : ScriptableObject
{
    [Header("EventListener Prefabs")]
    public List<EventListener> eventListenerPrefabs;

    [Header("View Prefabs")]
    public List<View> viewPrefabs;

    public List<EventListener> CreateEventListeners(Transform parent)
    {
        var instances = new List<EventListener>();
        foreach (var prefab in eventListenerPrefabs)
        {
            var instance = Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            instances.Add(instance);
        }
        return instances;
    }

    public List<View> CreateViews(Transform parent)
    {
        var instances = new List<View>();
        foreach (var prefab in viewPrefabs)
        {
            var instance = Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            instances.Add(instance);
        }
        return instances;
    }
}
