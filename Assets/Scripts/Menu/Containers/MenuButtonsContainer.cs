using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsContainer : ButtonContainer {
    public GameObject settingsMenu = null;

    protected override void SetupButtons()
    {
        StartSceneButton startButton = new StartSceneButton("Play!");
        OpenMenuButton settingsButton = new OpenMenuButton("Settings");
        //startButton.setScene("Game");
        settingsButton.setMenuTransition(this.gameObject, settingsMenu);
        AddButton(startButton);
        AddButton(settingsButton);
    }
}
