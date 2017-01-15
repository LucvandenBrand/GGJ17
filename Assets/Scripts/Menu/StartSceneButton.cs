using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This button loads a set scene when clicked. */
public class StartSceneButton : GUIButton {
    public Scene scene;

    protected override void DoAction()
    {
            SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }
}
