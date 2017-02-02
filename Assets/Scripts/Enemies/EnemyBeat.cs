using UnityEngine;

/* Enemy that dances to the beat of the music. */
public class EnemyBeat : Enemy
{
    [SerializeField]
    private float intensityTreshold = 0f;
    
    public new void Start()
    {
        base.Start();
    }

    public override void AudioImpact(float intensity)
    {
        this.curIntensity = intensity;
        this.ScaleBody(0.5f - intensity);
    }

    public void ScaleBody(float scale)
    {
        transform.localScale = new Vector3(scale * sizeScale, scale * sizeScale);
    }

    void Update()
    {
        if (this.curIntensity <= intensityTreshold)
            transform.Translate(movedirection * speed * curIntensity);
    }
}
