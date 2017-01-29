using System;
using System.Collections.Generic;
using UnityEngine;



public enum AudioImpactType {
    INTENSITY,
    SPEED,
    BASE_INTENSITY
}


/* Controls all audio elements. */
[RequireComponent( typeof(AudioSource) )]
public class AudioSystemController : MonoBehaviour {
    private static AudioSystemController instance = null;

    private List< KeyValuePair<AudioImpactListener, AudioImpactType> > audioImpactListeners = new List< KeyValuePair<AudioImpactListener, AudioImpactType> >();
    public AudioSource audioSource;
    private float alpha = 0;

    [SerializeField]
    private AnimationCurve intensityCurve = AnimationCurve.Linear( 0, 0.5f, 1, 1);
    [SerializeField]
    [Range(20, 40)]
    private int intensityMultiplier = 30;


    public void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if(audioSource == null)
        {
            return;
        }
        for (int i = 0; i < audioImpactListeners.Count; i++) {
            switch (audioImpactListeners[i].Value) {
                case AudioImpactType.INTENSITY:
                    audioImpactListeners[i].Key.AudioImpact( GetIntensity() );
                    break;
                case AudioImpactType.SPEED:
                    throw new NotImplementedException();
                    break;
                case AudioImpactType.BASE_INTENSITY:
                    audioImpactListeners[i].Key.AudioImpact( GetBaseIntensity() ); 
                    break;
            }
        }
    }


    private void LateUpdate() {
        if (audioSource.clip == null)
            clampPlayers();
    }


    /*
      Because the AudioSystemsController is the place that knows
      if the audioSource is already loaded,
      this is the easiest place to ensure that the players do not leave the area before
      the song has started.
    */
    void clampPlayers() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int index = 0; index < players.Length; ++index) {
            var pos = Camera.main.WorldToViewportPoint(players[index].transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
           players[index].transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }


    private float GetSpeed() {
        throw new NotImplementedException();
        return 5;
    }

    private float GetBaseIntensity() {
        if (audioSource.clip == null)
            return 0.02f;

        float[] spectrum = new float[128];
        float result = 0;

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < (spectrum.Length / 2f) - 1; ++i) {
            result += spectrum[i] *2;
        }

        return result / 128f * intensityCurve.Evaluate(  Remap(audioSource.time, 0, audioSource.clip.length, 0, 1) ) * intensityMultiplier;
    }

    private float GetIntensity() {
        if (audioSource.clip == null)
            return 0.02f;

        float[] spectrum = new float[128];
        float result = 0;

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; ++i) {
            result += spectrum[i];
        }

        return result / 128f * intensityCurve.Evaluate(  Remap(audioSource.time, 0, audioSource.clip.length, 0, 1) ) * intensityMultiplier;
    }


    public void AddAudioImpactListener( AudioImpactListener ail, AudioImpactType impactType = AudioImpactType.INTENSITY ) {
        audioImpactListeners.Add( new KeyValuePair<AudioImpactListener, AudioImpactType>(ail, impactType) );
    }

    public void RemoveAudioImpactListener( AudioImpactListener ail, AudioImpactType impactType = AudioImpactType.INTENSITY ) {
        audioImpactListeners.Remove( new KeyValuePair<AudioImpactListener, AudioImpactType>(ail, impactType) );
    }


    public static AudioSystemController GetAudioSystemController() {
        if (instance) 
            return instance;
        else {
            Debug.LogWarning( "!!! this singleton had no instance, a new Object is Generated - AudioSystemController::GetAudioSystemController " );
            return new GameObject().AddComponent<AudioSystemController>().gameObject.GetComponent<AudioSystemController>();  // wow
        }
    }

    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
