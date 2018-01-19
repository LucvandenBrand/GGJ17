using UnityEngine;

/* Objects implementing this class can have behavior dependent
 * on the music currently playing. */
public abstract class AudioImpactListener : MonoBehaviour
{
    /* This method is called whenever new music state data is available. */
    public abstract void AudioImpact( float reguestedCallbackValue );

    public void Start()
    {
        GetAudioSystemController().AddAudioImpactListener(this);
    }

    void OnDestroy()
    {
        GetAudioSystemController().RemoveAudioImpactListener(this);
    }

    public AudioSystemController GetAudioSystemController()
    {
        return AudioSystemController.GetAudioSystemController();
    }
}
