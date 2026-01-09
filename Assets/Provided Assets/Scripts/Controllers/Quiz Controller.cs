using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MenuManager;

public class QuizController : MonoBehaviour
{
    [SerializeField] Sprite unPressed;
    [SerializeField] Sprite correctOption;
    [SerializeField] Sprite correctOptionPressed;
    [SerializeField] Sprite wrongOption;
    [SerializeField] Sprite wrongOptionPressed;

    [SerializeField] Image[] options;

    private bool isAnimating = false;

    public void CorrectAnswer(int index)
    {
        if (isAnimating) return;
        AudioManager.Instance.PlaySound("Click");
        isAnimating = true;

        StartCoroutine(CorrectAnswerSpriteChange(index));   
    }

    IEnumerator CorrectAnswerSpriteChange(int index)
    {
        options[index].sprite = correctOptionPressed;

        yield return new WaitForSeconds(0.9f);

        options[index].sprite = correctOption;

        yield return new WaitForSeconds(1f);

        options[index].sprite = correctOption;

        // Get the active section
        Menu currentMenu = MenuManager.Instance.currentMenu;
        Sections section = MenuManager.Instance.GetMenu<Sections>(currentMenu);
        if (section != null)
        {
            isAnimating = false;

            if (currentMenu == Menu.Section3)
                section.EnableNext(5);
            if (currentMenu == Menu.Section4)
                section.EnableNext(4);
            if (currentMenu == Menu.Section5)
                section.EnableNext(5);
            if (currentMenu == Menu.Section6)
                section.EnableNext(6);

            for (int i = 0; i < options.Length; i++)
            {
                options[i].sprite = unPressed;
            }
        }
    }

    public void WrongAnswer(int index)
    {
        if (isAnimating) return;
        AudioManager.Instance.PlaySound("Click");
        isAnimating = true;

        StartCoroutine(WringAnswerSpriteChange(index));
    }

    IEnumerator WringAnswerSpriteChange(int index)
    {
        options[index].sprite = wrongOptionPressed;

        yield return new WaitForSeconds(1f);

        options[index].sprite = wrongOption;
        isAnimating = false;
    }
}
