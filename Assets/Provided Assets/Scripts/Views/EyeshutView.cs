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
            section.EnableNext(1);
    }

    public void OnAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
