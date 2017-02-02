using UnityEngine;

/* Enemy that dances in a sine wave. */
public class EnemySine : Enemy
{
    [SerializeField]
    private float intensityTreshold = 0f;
    [SerializeField]
    private float frequency = 20.0f;  // Speed of sine movement
    [SerializeField]
    private float magnitude = 0.5f;   // Size of sine movement
    private Vector3 linePos;
    private Vector3 axis;

    public new void Start()
    {
        base.Start();
        linePos = transform.position;
        axis = transform.right;
        transform.localScale = new Vector3(0.2f * sizeScale, 0.2f * sizeScale);
    }

    public override void AudioImpact(float intensity)
    {
        this.curIntensity = intensity;
        this.ScaleBody(Mathf.Clamp(intensity,0.1f, 0.2f));
    }

    public new void ScaleBody(float scale)
    {
        transform.localScale = new Vector3(scale * sizeScale, scale * sizeScale);
    }

    void Update()
    {
        linePos += movedirection * speed * curIntensity;
        transform.position = linePos + new Vector3(axis.y * Mathf.Sin(Time.time * frequency) * magnitude, axis.x * Mathf.Cos(Time.time * frequency), 0.0f);
    }
}
