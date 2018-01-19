using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* When this object is tapped, it will animate the menuEye object
 * and load lobby scene. */
[RequireComponent(typeof(AudioSource))]
public class IricButton : MonoBehaviour
{
    // Object to animate.
    [SerializeField]
    private GameObject menuEye;

    private const int MOUSE_LEFT = 0;

    // Tweakable constants for the animation.
    private const float PULSE_FREQUENCY = 10, PULSE_AMPLITUDE = 0.2f,
                        PULSE_MINSCALE = 2.5f;
    private const float LOAD_SCENE_DELAY = 0.5f;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(MOUSE_LEFT))
        {
            PulseAnimation anim = menuEye.AddComponent<PulseAnimation>();
            anim.SetFrequency(PULSE_FREQUENCY);
            anim.SetAmplitude(PULSE_AMPLITUDE);
            anim.SetMinScale(PULSE_MINSCALE);
            GetComponent<AudioSource>().Play();

            // Load the new scene, with a delay so the animation can play.
            StartCoroutine("StartGame", LOAD_SCENE_DELAY);
        }
    }

    IEnumerator StartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
