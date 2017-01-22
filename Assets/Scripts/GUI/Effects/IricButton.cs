using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IricButton : MonoBehaviour {


    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PulseAnimation anim = GameObject.Find("MenuEye").AddComponent<PulseAnimation>();
            anim.SetFrequency(10);
            anim.SetAmplitude(0.2f);
            anim.SetMinScale(3f);
            StartCoroutine("StartGame", 0.5f);
        }
    }

    IEnumerator StartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
