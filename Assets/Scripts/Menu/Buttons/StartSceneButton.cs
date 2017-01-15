using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This button loads a set scene when clicked. */
public class StartSceneButton : Button {
    private string scene;

    public StartSceneButton(string label) : base(label)
    {
        this.scene = SceneManager.GetActiveScene().name;
    }

    public void setScene(string scene)
    {
        this.scene = scene;
    }

    public override void DoAction()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
