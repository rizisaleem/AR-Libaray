using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ARSession arSession;
    public ARTrackedImageManager imageManager;
    public ARCameraBackground cameraBg;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        arSession.enabled = true;     // start AR early
        imageManager.enabled = false; // don't track yet
        cameraBg.enabled = false;     // hide camera
    }

    public void EnableARCamera()
    {
        MenuManager.Instance.ChangeMenu(MenuManager.Menu.None);
        EnableAR();
    }

    public void EnableAR()
    {
        if (!arSession.enabled)
            arSession.enabled = true;

        cameraBg.enabled = true;
        StartCoroutine(EnableARDelayed());
    }

    IEnumerator EnableARDelayed()
    {
        yield return new WaitForSeconds(0.3f);
        imageManager.enabled = true;
    }
    public void DisableAR()
    {
        imageManager.enabled = false;
        cameraBg.enabled = false;
    }
}
