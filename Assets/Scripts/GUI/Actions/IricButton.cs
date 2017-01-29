using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* When this object is tapped, it will animate the menuEye object
 * and load lobby scene. */
[RequireComponent(typeof(AudioSource))]
public class IricButton : MonoBehaviour {
    [SerializeField]
    private GameObject menuEye;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PulseAnimation anim = menuEye.AddComponent<PulseAnimation>();
            anim.SetFrequency(10);
            anim.SetAmplitude(0.2f);
            anim.SetMinScale(2.5f);
            GetComponent<AudioSource>().Play();
            StartCoroutine("StartGame", 0.5f);
        }
    }

    IEnumerator StartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
