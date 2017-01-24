using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent( typeof(Camera) )]
[RequireComponent( typeof(BloomOptimized) )]
public class BloomImpactListener : AudioImpactListener {

    [SerializeField]
    private FloatRange bloomIntancity;

    private BloomOptimized bloomOptimized;



    void Awake() {
        bloomOptimized = gameObject.GetComponent<BloomOptimized>();
    }

    public override void AudioImpact( float intensity ) {
        intensity = Mathf.Clamp( intensity,  0, 1);
//        float tmp = Remap(intensity, 0, 1,  audioIntancity.min, audioIntancity.max);
        bloomOptimized.intensity = Remap(intensity, 0, 1,  bloomIntancity.min, bloomIntancity.max);
    }

    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}




