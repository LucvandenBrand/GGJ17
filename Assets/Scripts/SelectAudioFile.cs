using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAudioFile : MonoBehaviour {
    public string audioPath;
    public AudioSource audioSource;
    public AudioImageImporter aii;
    private AudioClip resultClip;
	// Use this for initialization
	void Start () {
        //string url = "file:///" + audioPath;
        //WWW www = new WWW(url);
        Debug.Log("startSelection");
        StartCoroutine("LoadMP3");
        //audioSource.clip = NAudioPlayer.FromMp3Data(www.bytes);
    }

    IEnumerator LoadMP3()
    {
        string url = "file:///" + audioPath;
        WWW www = new WWW(url);
        Debug.Log("starting");
        while (!www.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        resultClip = NAudioPlayer.FromMp3Data(www.bytes);
        audioSource.clip = resultClip;
        Debug.Log("done");
        StartAudioImageImporter();
        yield return null;
    }

    private void StartAudioImageImporter()
    {
        Debug.Log("size = " + resultClip.length);
        aii.ProcessAudioFile(audioPath, resultClip.length);
    }

    void Update()
    {
        if (audioSource.clip != null) { 
            if (!audioSource.isPlaying && audioSource.clip.isReadyToPlay)
                audioSource.Play();
        }
    }
}
