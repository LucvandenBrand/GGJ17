using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraImpactListener : AudioImpactListener {

    float size;


    void Start() {
        size = Camera.main.orthographicSize;
    }

    public override void AudioImpact( float reguestedCallbackValue ) {
        Debug.Log( "AudioImpact : " + reguestedCallbackValue );
        Camera.main.orthographicSize = size * reguestedCallbackValue;
    }

    
//    public void Start() {
//        AudioSystemController.GetAudioSystemController().AddAudioImpactListener( this, AudioImpactType.BASE_INTENSITY);
//    }
}
