using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuButton : GUIButton {
    public GameObject fromMenu, toMenu;

    protected override void DoAction()
    {
        if (toMenu != null)
            toMenu.SetActive(true);
        if (fromMenu != null)
            fromMenu.SetActive(false);
    }
}
