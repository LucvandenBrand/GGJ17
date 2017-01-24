using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibleScoreCounter : AudioImpactListener {

    // Use this for initialization
    new void Start () {
        base.Start();
//        GetAudioSystemController().AddAudioImpactListener(this); 
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    public override void AudioImpact(float reguestedCallbackValue)
    {
        if (GetAudioSystemController().audioSource.clip) {
            int rawSeconds = Mathf.FloorToInt(GetAudioSystemController().audioSource.time);
            int totalDuration = Mathf.FloorToInt(GetAudioSystemController().audioSource.clip.length);
            gameObject.transform.GetComponent<Text>().text = formatSeconds(rawSeconds) + " / " + formatSeconds(totalDuration);
        }
    }

    private string formatSeconds(int rawSeconds)
    {

        int minutes = rawSeconds / 60;
        int seconds = rawSeconds % 60;
        string secondsStr = "";
        secondsStr += minutes + ":";
        if (seconds < 10)
        {
            secondsStr += "0";
        }
        return secondsStr += seconds;
    }
}
