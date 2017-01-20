using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls all audio elements. */
public class AudioSystemController : MonoBehaviour {
    public List<AudioImpactListener> audioImpactListeners;
    private float alpha = 0;
	
	// Update is called once per frame
	void Update () {
        foreach (AudioImpactListener listener in audioImpactListeners)
            listener.AudioImpact(GetSpeed(), GetIntensity());
	}

    float GetSpeed()
    {
        return 25;
    }

    float GetIntensity()
    {
        float speed = 10;
        this.alpha += speed;
        return 10 * Mathf.Cos(alpha) + 2 * Mathf.Sin(alpha) * Mathf.Tan(alpha);
    }
}
