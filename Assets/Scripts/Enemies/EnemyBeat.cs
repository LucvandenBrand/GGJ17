using UnityEngine;
using System.Collections;

public class EnemyBeat : Enemy
{

    [SerializeField]
    private float intensityTreshold = 0f;
    
    public new void Start()
    {
        //destroyAfterSeconds = destroyBeatEnemyAfterSeconds;
        base.Start();
    }

    public override void AudioImpact(float intensity)
    {
        this.curIntensity = intensity;

        //if (this.curIntensity >= intensityTreshold)
        //{
            this.ScaleBody(0.5f - 1.0f * intensity);
        //}

    }

    public void ScaleBody(float scale)
    {
        transform.localScale = new Vector3(scale * sizeScale, scale * sizeScale);
    }

    void Update()
    {
        if (this.curIntensity <= intensityTreshold)
        {
            transform.Translate(movedirection * speed * curIntensity);
        }
    }
}
