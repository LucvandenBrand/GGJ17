using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent( typeof(Camera) )]
[RequireComponent( typeof(BloomOptimized) )]
public class BloomImpactListener : AudioImpactListener {

    [Header("these values")]
    public FloatRange audioIntancity;
    [Header("remap to these values")]
    public FloatRange bloomIntancity;

    private BloomOptimized bloomOptimized;



    void Awake() {
        bloomOptimized = gameObject.GetComponent<BloomOptimized>();
    }

    public override void AudioImpact( float speed, float intensity ) {
        float tmp = remap(intensity, 0, 1,  audioIntancity.min, audioIntancity.max);
        bloomOptimized.intensity = remap(tmp, audioIntancity.min, audioIntancity.max,  bloomIntancity.min, bloomIntancity.max);
    }

    private float remap (float v, float a1, float a2, float b1, float b2) {
        return (v - a1) / (b2 - a1) * (b2 - a1) + a2;;
    }
}




[SerializeField]
public class FloatRange {
    [Range(0,1)]
    public float min;
    [Range(0,1)]
    public float max;
    

    public FloatRange(float min, float max) {
        this.min = min;
        this.max = max;
    }

    public float GetRandomBetween() {
        return Random.Range(min, max);
    }
}