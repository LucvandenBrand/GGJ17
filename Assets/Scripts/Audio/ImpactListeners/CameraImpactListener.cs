using UnityEngine;

/* Makes the Camera `bounce' to the music. */
public class CameraSizeImpactListener : AudioImpactListener
{
    private float startSize;

    public void Start()
    {
        base.Start();
        this.startSize = Camera.main.orthographicSize;
    }

    public override void AudioImpact( float reguestedCallbackValue )
    {
        Camera.main.orthographicSize = startSize * reguestedCallbackValue;
    }
}
