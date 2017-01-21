using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent( typeof(Camera) )]
[RequireComponent( typeof(BloomOptimized) )]
public class BloomImpactListener : AudioImpactListener {

    [Header("these values")]
    [SerializeField]
    private FloatRange audioIntancity;

    [Header("are remap to these values")]
    [SerializeField]
    private FloatRange bloomIntancity;

    private BloomOptimized bloomOptimized;



    void Awake() {
        bloomOptimized = gameObject.GetComponent<BloomOptimized>();
    }

    public override void AudioImpact( float intensity ) {
        intensity = Mathf.Clamp( intensity,  audioIntancity.min, audioIntancity.max);
        float tmp = Remap(intensity, 0, 1,  audioIntancity.min, audioIntancity.max);
        bloomOptimized.intensity = Remap(tmp, audioIntancity.min, audioIntancity.max,  bloomIntancity.min, bloomIntancity.max);
    }

    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}




[System.Serializable]
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