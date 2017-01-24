using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderMusicImpacter : AudioImpactListener {
    [Range(1, 20)]
    private int takeAverageOf = 15;
    private FloatAverage average;
    private Material mat;

    void Awake() {
        mat = GetComponent<Renderer>().material;
        average = new FloatAverage( takeAverageOf );
    }

    public override void AudioImpact( float intensity ) {
        average.Add(intensity);
        mat.SetFloat("_MusicIntensity", average.GetAverage());
    }
}
