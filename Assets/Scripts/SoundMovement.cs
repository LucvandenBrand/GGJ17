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

    public override void AudioImpact(float speed, float intensity)
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
        float movement = (curIntensity - 0.25f) * 2;
        if (movement < 0)
        {
            movement = 0;
        }
        transform.Translate(direction * movement);
    }
}
