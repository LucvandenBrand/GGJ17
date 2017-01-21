using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls all audio elements. */
public class AudioSystemController : MonoBehaviour {
    public List<AudioImpactListener> audioImpactListeners;
    public AudioSource audioSource;
    public AudioImageImporter aii;
    private float alpha = 0;
	
	// Update is called once per frame
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
}
