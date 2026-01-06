using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public MenuManager.ViewType ViewType;

    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
}
