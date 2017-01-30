using UnityEngine;

/* Allows shaders with the property MusicIntensity to be
 * dependant on music. */
public class ShaderMusicImpacter : AudioImpactListener {
    [Range(1, 20)]
    private int takeAverageOf = 15;
    private FloatAverage average;
    private Material mat;

    void Awake()
    {
        mat = GetComponent<Renderer>().material;
        average = new FloatAverage( takeAverageOf );
    }

    public override void AudioImpact( float intensity )
    {
        average.Add(intensity);
        mat.SetFloat("_MusicIntensity", average.GetAverage());
    }
}
