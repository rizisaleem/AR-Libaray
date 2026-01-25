using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static MenuManager;

public class ARCameraManager : MonoBehaviour
{
    public static ARCameraManager Instance;

    [SerializeField] private bool isDebugMode;
    private int count;

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
        count = 1;
        arSession.enabled = true;     // start AR early
        imageManager.enabled = false; // don't track yet
        cameraBg.enabled = false;     // hide camera
    }

    public void EnableARCamera()
    {
        MenuManager.Instance.ChangeMenu(MenuManager.Menu.None);

        if (isDebugMode)
            MenuManager.Instance.EnableView(ViewType.TestView, true);

        StartCoroutine(EnableARDelayed());
        EnableAR();
    }

    public void EnableAR()
    {
        AudioManager.Instance.TurnMusicOnOff(false);

        if (!arSession.enabled)
            arSession.enabled = true;

        cameraBg.enabled = true;
    }

    IEnumerator EnableARDelayed()
    {
        yield return new WaitForSeconds(0.3f);
        imageManager.enabled = true;
    }

    public void DisableAR(MenuManager.Menu menu)
    {
        MenuManager.Instance.ChangeMenu(menu);
        DisableARCamera();
    }

    public void DisableARCamera()
    {
        AudioManager.Instance.TurnMusicOnOff(true);
        imageManager.enabled = false;
        cameraBg.enabled = false;
    }

    public void EnableTestView()
    {
        var menus = (MenuManager.Menu[])System.Enum.GetValues(typeof(MenuManager.Menu));

        // Skip None
        int validMenuCount = menus.Length - 1;

        // Reset if overflow
        if (count >= validMenuCount)
            count = 0;

        MenuManager.Menu nextMenu = menus[count];
        DisableAR(nextMenu);

        count++;
        MenuManager.Instance.EnableView(ViewType.TestView, false);

        // Get the active section
        Sections section = MenuManager.Instance.GetMenu<Sections>(MenuManager.Instance.currentMenu);
        if (section != null && section.MenuType != MenuManager.Menu.Section1)
            section.PlayAnimation();
    }
}
