using System;
using System.Collections.Generic;
using UnityEngine;



public enum AudioImpactType {
    INTENSITY,
    SPEED,
    BASE_INTENSITY
}


/* Controls all audio elements. */
public class AudioSystemController : ScriptableObject {
    private static AudioSystemController instance = null;

    private List< KeyValuePair<AudioImpactListener, AudioImpactType> > audioImpactListeners = new List< KeyValuePair<AudioImpactListener, AudioImpactType> >();
//    private Dictionary<AudioImpactListener, AudioImpactType> dictionary = new Dictionary<AudioImpactListener, AudioImpactType>();
    public AudioSource audioSource;
    public AudioImageImporter aii;
    private float alpha = 0;

    public void Awake() {
        instance = this;
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


    void clampPlayers() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int index = 0; index < players.Length; ++index) {
            var pos = Camera.main.WorldToViewportPoint(players[index].transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
           players[index].transform.position = Camera.main.ViewportToWorldPoint(pos);
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
            return new AudioSystemController();
        }
    }
}
