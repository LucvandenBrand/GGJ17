using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioImpactListener : MonoBehaviour {
    public abstract void AudioImpact( float reguestedCallbackValue );

    public void Start() {
        Debug.Log( "Ik Start" );
        GetAudioSystemController().AddAudioImpactListener(this);
    }

    void OnDestroy() {
        GetAudioSystemController().RemoveAudioImpactListener(this);
    }

    public AudioSystemController GetAudioSystemController()
    {
        return AudioSystemController.GetAudioSystemController();
    }
}
