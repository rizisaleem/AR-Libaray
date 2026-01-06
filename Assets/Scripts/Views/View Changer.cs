using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ViewChanger : MonoBehaviour
{
    public ARSession arSession;
    public ARTrackedImageManager imageManager;
    public ARCameraBackground cameraBg;

    void Start()
    {
        arSession.enabled = true;     // start AR early
        imageManager.enabled = false; // don't track yet
        cameraBg.enabled = false;     // hide camera
    }

    public void DisableAR()
    {
        cameraBg.enabled = false;
        imageManager.enabled = false;
    }

    public void EnableAR()
    {
        if (!arSession.enabled)
            arSession.enabled = true;

        cameraBg.enabled = true;
        StartCoroutine(EnableARDelayed());

        MenuManager.Instance.EnableView(MenuManager.ViewType.View1, false);
    }

    IEnumerator EnableARDelayed()
    {
        yield return new WaitForSeconds(0.3f);
        imageManager.enabled = true;
        MenuManager.Instance.EnableView(MenuManager.ViewType.View2, true);
    }

    public void View1()
    {
        DisableAR();
        MenuManager.Instance.ShowOnlyView(MenuManager.ViewType.View1);
    }

    public void EnableARFresh()
    {
        arSession.enabled = false;
        arSession.enabled = true;

        imageManager.enabled = true;
        cameraBg.enabled = true;
    }

    public void EnableSlideMenu()
    {
        MenuManager.Instance.ChangeMenu(MenuManager.Menu.Gameplay);
        MenuManager.Instance.ShowOnlyView(MenuManager.ViewType.View3);
    }

    public void EnableNextSlide()
    {
        MenuManager.Instance.ShowOnlyView(MenuManager.ViewType.View4);
    }

    public void EnableHomeMenu()
    {
        MenuManager.Instance.ChangeMenu(MenuManager.Menu.Home);
        MenuManager.Instance.ShowOnlyView(MenuManager.ViewType.View1);
    }
}
