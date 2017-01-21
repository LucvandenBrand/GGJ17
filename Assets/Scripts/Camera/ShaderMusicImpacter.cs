using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderMusicImpacter : AudioImpactListener {
    private FloatAverage average = new FloatAverage(15);
    public override void AudioImpact(float intensity)
    {
        average.Add(intensity);
    }

    public void Update()
    {
        GetComponent<Renderer>().material.SetFloat("_MusicIntensity", average.GetAverage());
    }
}
