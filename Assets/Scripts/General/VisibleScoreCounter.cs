using UnityEngine;
using UnityEngine.UI;

/* Shows a counter representing the current place in the song. */
[RequireComponent(typeof(Text))]
public class VisibleScoreCounter : AudioImpactListener {
    /* Every audioimpact, update the score counter. */
    public override void AudioImpact(float reguestedCallbackValue)
    {
        AudioSource audioSource = GetAudioSystemController().GetAudioSource();
        if (audioSource.clip) {
            int rawSeconds = Mathf.FloorToInt(audioSource.time);
            int totalDuration = Mathf.FloorToInt(audioSource.clip.length);
            gameObject.transform.GetComponent<Text>().text = SecondFormatter.FormatSeconds(rawSeconds) + " / " 
                                                           + SecondFormatter.FormatSeconds(totalDuration);
        }
    }
}
