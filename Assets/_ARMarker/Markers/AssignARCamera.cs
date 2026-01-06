using UnityEngine;

public class AssignARCamera : MonoBehaviour
{
    void Start()
    {
        var canvas = GetComponent<Canvas>();
        if (canvas != null && canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
