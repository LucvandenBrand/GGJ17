using System;
using System.Collections.Generic;
using UnityEngine;

/* All types of music analysis information we can
 * return to our listeners. */
public enum AudioImpactType {
    INTENSITY,
    SPEED,
    BASS_INTENSITY
}

/* Controls all audio elements based on the music currently playing.
 * Implemented as a singleton. */
[RequireComponent( typeof(AudioSource) )]
public class AudioSystemController : MonoBehaviour {
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AnimationCurve intensityCurve = AnimationCurve.Linear(0, 0.5f, 1, 1);
    [SerializeField]
    [Range(20, 40)]
    private int intensityMultiplier = 30;
    [SerializeField]
    private int FFTPrecision = 5; // the higher, the preciser the FFT-data, and the slower the game.

    private static AudioSystemController instance = null;
    private List< KeyValuePair<AudioImpactListener, AudioImpactType> > audioImpactListeners = new List< KeyValuePair<AudioImpactListener, AudioImpactType> >();
    private float alpha = 0;
    private int FFTSampleSize; // 2 << FFTPrecision.

    public void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        FFTSampleSize = 2 << FFTPrecision;
    }

    /* Every frame, call all listeners with the current intesity of the music.*/
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
                    audioImpactListeners[i].Key.AudioImpact(GetSpeed());
                    break;
                case AudioImpactType.BASS_INTENSITY:
                    audioImpactListeners[i].Key.AudioImpact( GetBassIntensity() ); 
                    break;
            }
        }
    }

    /* Make sure players do not leave the screen when no music is playing. */
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

    /* Returns the speed of the music currently playing. */
    private float GetSpeed() {
        throw new NotImplementedException();
    }

    /*
      Returns the unmodified intensity of the song at this point in time.
      upperLimit should be a number between 0.0 and 1.0,
      if 0.5, only the lower 0.5 part of the spectrum is used.
     */
    private float GetRawIntensity(float upperLimit) {
        if (audioSource.clip == null)
            return 0.0001f;

        float[] spectrum = new float[FFTSampleSize];
        float result = 0;

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < (spectrum.Length * upperLimit); ++i) {
            result += spectrum[i] / upperLimit;
        }
        return result / FFTSampleSize;
    }

    /*
      Returns the intensity of the lower half of the spectrum.
    */
    private float GetBassIntensity() {
        return GetIntensity(0.5f);
    }

    /*
      Returns the intensity of the whole spectrum,
      multiplied with the intensity curve.
     */
    private float GetIntensity(float upperLimit = 1.0f) {
        if (audioSource.clip == null)
            return 0.0001f;

        return GetRawIntensity(upperLimit) * intensityCurve.Evaluate(  Remap(audioSource.time, 0, audioSource.clip.length, 0, 1) ) * intensityMultiplier;
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

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
