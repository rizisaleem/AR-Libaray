using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sections : EventListener
{
    public void EnableNextSlide(int index)
    {
        if (slides == null || slides.Length == 0)
        {
            Debug.LogWarning("Slides array is empty or not assigned.");
            return;
        }

        if (index < 0 || index >= slides.Length)
        {
            Debug.LogWarning("Invalid Slide index: " + index);
            return;
        }

        // Disable all slides
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);
        }

        slides[index].SetActive(true);
    }

    public void LastSlide(int index)
    {
        if (slides == null || slides.Length == 0)
        {
            Debug.LogError("Slides array is NULL or empty in LastSlide()");
            return;
        }

        if (index < 0 || index >= slides.Length)
        {
            Debug.LogError("Invalid slide index: " + index);
            return;
        }

        slides[index].SetActive(false);
        slides[0].SetActive(true);

        GameManager.Instance.EnableARCamera();
    }
}
