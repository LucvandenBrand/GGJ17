using UnityEngine;
using System.Collections;

public class Enemy : AudioImpactListener {

    [HideInInspector]
	public Vector3 movedirection;
    [HideInInspector]
    public float speed;
    public float curIntensity = 0;

    public void Start()
    {
        base.Start();
    }

    public override void AudioImpact(float speed, float intensity)
    {
        this.curIntensity = intensity;
    }


    void Update() {
        transform.Translate( movedirection * speed * curIntensity );
    }
}
