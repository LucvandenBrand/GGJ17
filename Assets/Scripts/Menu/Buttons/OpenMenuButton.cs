using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuButton : Button {
    private GameObject fromMenu, toMenu;

    public OpenMenuButton(string label) : base(label)
    {
        this.fromMenu = null;
        this.toMenu = null;
    }

    public override void DoAction()
    {
        if (toMenu != null && fromMenu != null)
        {
            toMenu.SetActive(true);
            fromMenu.SetActive(false);
        }
    }

    public void setMenuTransition(GameObject fromMenu, GameObject toMenu)
    {
        this.fromMenu = fromMenu;
        this.toMenu = toMenu;
    }
}
