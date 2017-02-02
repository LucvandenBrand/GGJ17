using UnityEngine;
using System.Collections;

/* An object in the game that is out to hurt the player. 
 * Is dependant on the music, and thus an AudioImpactListener. */
public class Enemy : AudioImpactListener
{
    [SerializeField]
    protected float destroyAfterSeconds = 5f;
    [SerializeField]
    protected float enemySpeed = 1.0f;
    [SerializeField]
    protected float minScale = 0.1f;
    [SerializeField]
    protected float maxScale = 0.7f;
    [SerializeField]
    protected float maxSpeed = 0.1f;
    protected Vector3 movedirection;
    protected float speed;
    protected float curIntensity = 0;
    protected float sizeScale = 2;

    public void Start()
    {
        base.Start();
        speed = enemySpeed;
        movedirection = -transform.position.normalized;
        gameObject.AddComponent<DestroyAfter>().SetDestroyDelay(destroyAfterSeconds);
    }

    public override void AudioImpact(float intensity)
    {
        this.curIntensity = intensity;
        this.ScaleBody(intensity);
    }

    public void ScaleBody(float scale)
    {
        scale *= sizeScale;
        scale = Mathf.Clamp(scale, minScale, maxScale);
        transform.localScale = new Vector3(scale, scale);
    }

    void Update()
    {
        float clamped_speed = Mathf.Clamp(speed * curIntensity, 0.0f, maxSpeed);
        transform.Translate(movedirection * clamped_speed);
    }
}
