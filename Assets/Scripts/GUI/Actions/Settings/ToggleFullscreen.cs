using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* An UI toggle that allows you to change 
 * wether or not the game is played in fullscreen mode. */
public class ToggleFullscreen : Toggle {

	protected override void Start () {
        base.Start();
        this.isOn = Screen.fullScreen;
        this.onValueChanged.AddListener(delegate {
            Screen.fullScreen = this.isOn;
        });
    }
}
