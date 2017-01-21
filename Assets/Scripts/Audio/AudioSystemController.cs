using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls all audio elements. */
public class AudioSystemController : MonoBehaviour {
    private static AudioSystemController instance = null;

    public List<AudioImpactListener> audioImpactListeners;
    public AudioSource audioSource;
    public AudioImageImporter aii;
    private float alpha = 0;
	
	

    public void Awake()
    {
        instance = this;
    }

	void Update () {
        foreach (AudioImpactListener listener in audioImpactListeners)
            listener.AudioImpact(GetSpeed(), GetIntensity());
	}

    float GetSpeed()
    {
        return 5;
    }

    float GetIntensity()
    {
        return aii.GetIntensity(audioSource.time);
    }

    public void AddAudioImpactListener(AudioImpactListener ail)
    {
        audioImpactListeners.Add(ail);
    }

    public static AudioSystemController GetAudioSystemController()
    {
        return instance;
    }
}
