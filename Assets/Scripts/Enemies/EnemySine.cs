using UnityEngine;
using System.Collections;

public class EnemySine : Enemy
{

    [SerializeField]
    private float intensityTreshold = 0f;

    public float frequency = 20.0f;  // Speed of sine movement
    public float magnitude = 0.5f;   // Size of sine movement

    private Vector3 linePos;
    private Vector3 axis;

    public new void Start()
    {
        linePos = transform.position;
        //destroyAfterSeconds = destroyBeatEnemyAfterSeconds;
        base.Start();
        axis = transform.right;
        transform.localScale = new Vector3(0.2f * sizeScale, 0.2f * sizeScale);
    }

    public override void AudioImpact(float intensity)
    {
        this.curIntensity = intensity;

        //if (this.curIntensity >= intensityTreshold)
        //{
        this.ScaleBody(Mathf.Clamp(intensity,0.1f, 0.2f));
        //}

    }

    public new void ScaleBody(float scale)
    {
        transform.localScale = new Vector3(scale * sizeScale, scale * sizeScale);
    }

    void Update()
    {
        /* if (this.curIntensity <= intensityTreshold)
         {
             transform.Translate(movedirection * speed * curIntensity);
         }*/
        linePos += movedirection * speed * curIntensity;
        //transform.position = linePos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        transform.position = linePos + new Vector3(axis.y * Mathf.Sin(Time.time * frequency) * magnitude, axis.x * Mathf.Cos(Time.time * frequency), 0.0f);
    }
}
