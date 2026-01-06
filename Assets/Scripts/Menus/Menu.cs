using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventListener : MonoBehaviour
{
    public MenuManager.Menu MenuType;

    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
}
