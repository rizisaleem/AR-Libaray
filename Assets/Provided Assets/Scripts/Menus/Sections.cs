using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MenuManager;

public class Sections : EventListener
{
    private Animator anim;
    private int currentSlide;
    private bool coolDown = true;

    private void Awake()
    {
            anim = GetComponent<Animator>();
    }

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

        if (!coolDown) return;
        coolDown = false;
        StartCoroutine(EnableCoolDown());

        AudioManager.Instance.PlaySound("Click");
        EnableNext(index);
    }

    IEnumerator EnableCoolDown()
    {
        yield return new WaitForSeconds(2f);
        coolDown = true;
    }

    public void EnableNext(int index)
    {
        currentSlide = index;
        slides[index - 1].SetActive(false);
        slides[index].SetActive(true);
    }

    public int getSlideIndex()
    {
        return currentSlide;
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

        if (!coolDown) return;

        AudioManager.Instance.PlaySound("Click");

        currentSlide = 0;
        slides[index].SetActive(false);
        slides[0].SetActive(true);

        ARCameraManager.Instance.EnableARCamera();
    }

    public void PlayAnimation()
    {
        StartCoroutine(NoteAnimation());
    }

    IEnumerator NoteAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("PlayAnim");
    }

    public void EnableEyeShutView()
    {
        MenuManager.Instance.EnableView(ViewType.Eyeshut, true);
    }

    public void OnAnimationComplete()
    {
        anim.SetTrigger("PlayIdle");
    }
}
