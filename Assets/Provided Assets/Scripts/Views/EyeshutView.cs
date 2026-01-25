using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MenuManager;

public class EyeshutView : View
{
    public void EnableNextSlide()
    {
        Sections section = MenuManager.Instance.GetMenu<Sections>(MenuManager.Instance.currentMenu);
        if (section != null)
        {
            int index = section.getSlideIndex() + 1;
            section.EnableNext(index);
            if(index == 1)
                section.GetComponent<Animator>().Play("BookAnim");
        }
    }

    public void OnAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
