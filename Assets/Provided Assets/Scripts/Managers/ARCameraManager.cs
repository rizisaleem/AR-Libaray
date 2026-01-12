using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static MenuManager;

public class ARCameraManager : MonoBehaviour
{
    public static ARCameraManager Instance;

    public ARSession arSession;
    public ARTrackedImageManager imageManager;
    public ARCameraBackground cameraBg;

    public GameObject modelPrefab;
    private GameObject spawnedModel;
    public Transform arCamera;

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
        AudioManager.Instance.TurnMusicOnOff(false);
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
    public void DisableAR(Menu menu)
    {
        AudioManager.Instance.TurnMusicOnOff(true);
        MenuManager.Instance.ChangeMenu(menu);

        imageManager.enabled = false;
        cameraBg.enabled = false;
    }

    public void Enable3DModel()
    {
        EnableARCamera();
        imageManager.enabled = false;

        if (spawnedModel == null)
            spawnedModel = Instantiate(modelPrefab);

        // Place model 1 meter in front of camera
        spawnedModel.transform.position =
            arCamera.position + arCamera.forward * 1.0f;

        spawnedModel.transform.rotation =
            Quaternion.LookRotation(arCamera.forward);

        spawnedModel.SetActive(true);

        StartCoroutine(Disable3DModel());
    }

    IEnumerator Disable3DModel()
    {
        yield return new WaitForSeconds(30f);

        if (spawnedModel != null)
            spawnedModel.SetActive(false);

        DisableAR(MenuManager.Menu.Section2);
    }
}
