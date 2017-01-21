using UnityEngine;
using System.Collections;

public class Enemy : AudioImpactListener
{

    [HideInInspector]
    public Vector3 movedirection;
    [HideInInspector]
    public float speed;
    public float curIntensity = 0;
    [HideInInspector]
    public float sizeScale = 2;

    public void Start()
    {
        base.Start();
    }

    public override void AudioImpact(float speed, float intensity)
    {
        this.curIntensity = intensity;
        this.ScaleBody(intensity);
    }

    public void ScaleBody(float scale)
    {
        transform.localScale = new Vector3(scale*sizeScale, scale*sizeScale);
    }

    void Update()
    {
        transform.Translate(movedirection * speed * curIntensity);
    }
}
