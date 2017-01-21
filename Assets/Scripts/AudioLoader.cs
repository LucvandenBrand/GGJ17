using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour {
    bool isAudioLoadRunning = false;
    public SelectAudioFile selectAudioFile;

    void Start () {
        LoadNewAudio();
    }
	
    void LoadNewAudio()
    {
        if (isAudioLoadRunning)
        {
            StopCoroutine(run());
        }
        StartCoroutine(run());
    }

    IEnumerator run()
    {
        isAudioLoadRunning = true;
        selectAudioFile.SelectAudio();
        isAudioLoadRunning = false;
        yield return null;
    }
}
