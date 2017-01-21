using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;



public enum AudioImpactType {
    INTENSITY,
    SPEED,
    BASE_INTENSITY
}


/* Controls all audio elements. */
public class AudioSystemController : MonoBehaviour {
    private static AudioSystemController instance = null;

    private List<AudioImpactListener> audioImpactListeners = new List<AudioImpactListener>();
    private Dictionary<AudioImpactListener, AudioImpactType> dictionary;
    public AudioSource audioSource;
    public AudioImageImporter aii;
    private float alpha = 0;



    public void Awake() {
        instance = this;
    }

    void Update() {
        foreach (AudioImpactListener listener in audioImpactListeners) {
            switch (dictionary[listener]) {
                case AudioImpactType.INTENSITY:
                    listener.AudioImpact(GetIntensity());
                    break;
                case AudioImpactType.SPEED:
                    throw new NotImplementedException();
                    break;
                case AudioImpactType.BASE_INTENSITY:
                    throw new NotImplementedException();
                    break;
            }
        }
    }

    float GetSpeed() {
        throw new NotImplementedException();
        return 5;
    }

    float GetBaseIntensity() {
        return aii.GetBaseIntensity(audioSource.time);
    }

    float GetIntensity() {
        return aii.GetIntensity(audioSource.time);
    }

    public void AddAudioImpactListener( AudioImpactListener ail, AudioImpactType impactType = AudioImpactType.INTENSITY ) {
        audioImpactListeners.Add(ail);
        dictionary.Add(ail, impactType);
    }

    public void RemoveAudioImpactListener( AudioImpactListener ail, AudioImpactType impactType = AudioImpactType.INTENSITY ) {
        audioImpactListeners.Remove(ail);
        dictionary.Add(ail, impactType);
    }

    public static AudioSystemController GetAudioSystemController() {
        if (instance) 
            return instance;
        else {
            Debug.LogWarning( "!!! this singleton had no instance, a new Object is Generated - AudioSystemController::GetAudioSystemController " );
            return new AudioSystemController();
        }
    }
}
