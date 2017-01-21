using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMovement : AudioImpactListener
{
    private float baseSpeed = 1;
    private Vector3 direction = Vector3.right;
    private float curIntensity;

    void Start()
    {
        base.Start();
    }

    public override void AudioImpact(float intensity)
    {
        curIntensity = intensity;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction * curIntensity);
    }
}
