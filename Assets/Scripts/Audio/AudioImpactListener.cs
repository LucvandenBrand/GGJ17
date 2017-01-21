using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioImpactListener : MonoBehaviour {
    public abstract void AudioImpact( float reguestedCallbackValue );

    public void Start() {
        AudioSystemController.GetAudioSystemController().AddAudioImpactListener(this);
    }

    void OnDestroy() {
        AudioSystemController.GetAudioSystemController().RemoveAudioImpactListener(this);
    }
}
