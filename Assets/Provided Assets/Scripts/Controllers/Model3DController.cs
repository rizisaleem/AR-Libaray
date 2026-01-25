using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MenuManager;

public class Model3DController : MonoBehaviour
{
    public GameObject modelPrefab;
    public Transform modelPlaceholder;
    private GameObject spawnedModel;

    public GameObject water;
    public GameObject Section2_Bg;
    public GameObject Section6_Bg;

    public void Enable3DModel()
    {
        ARCameraManager.Instance.EnableAR();
        water.SetActive(true);

        Sections section = MenuManager.Instance.GetMenu<Sections>(MenuManager.Instance.currentMenu);
        int index = section.getSlideIndex() + 1;
        section.EnableNextSlide(index);
        Section2_Bg.SetActive(false);
        Section6_Bg.SetActive(false);
        if (section != null && section.MenuType != MenuManager.Menu.Section2) return;

        if (spawnedModel == null)
            spawnedModel = Instantiate(modelPrefab);

        spawnedModel.transform.position = modelPlaceholder.position;
        spawnedModel.transform.rotation = modelPlaceholder.rotation;

        spawnedModel.SetActive(true);

        StartCoroutine(MoveModel());
    }

    IEnumerator MoveModel()
    {
        // Camera-relative directions
        Vector3 leftDir = -modelPlaceholder.right;

        // Control movement strength
        float leftSpeed = 0.65f;

        while (true)
        {
            Vector3 movement = (leftDir * leftSpeed) * Time.deltaTime;
            spawnedModel.transform.position += movement;

            yield return null;
        }
    }

    public void Disable3DModel()
    {
        MenuManager.Instance.EnableView(ViewType.Eyeshut, true);

        if (spawnedModel != null)
            spawnedModel.SetActive(false);

        water.SetActive(false);
        ARCameraManager.Instance.DisableARCamera();

        Invoke("EnableBG", 1f);
    }

    public void EnableBG()
    {
        Section2_Bg.SetActive(true);
        Section6_Bg.SetActive(true);
    }
}
