using UnityEngine;
using System.Collections;

public class Enemy : AudioImpactListener
{

    [HideInInspector]
    public Vector3 movedirection;
    [HideInInspector]
    public float speed;
    protected float curIntensity = 0;
    [HideInInspector]
    public float sizeScale = 2;
    [SerializeField]
    protected float destroyAfterSeconds = 5f;
    [SerializeField]
    protected float enemySpeed = 1.0f;

    public void Start()
    {
        base.Start();
        speed = enemySpeed;
        movedirection = -transform.position.normalized;

        //destroyAfterSeconds = 5f;

        gameObject.AddComponent<DestroyAfter>().destroyAfter = destroyAfterSeconds;
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
