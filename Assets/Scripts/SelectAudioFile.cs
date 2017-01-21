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
        LoadAudioFile(audioPath);
    }

    public void LoadAudioFile(string audioPath)
    {
        StartCoroutine(LoadAudio(audioPath));
        
    }

    IEnumerator LoadAudio(string audioPath)
    {
        string url = "file:///" + audioPath;
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        if (audioPath.EndsWith(".mp3"))
        {
            Debug.Log("mp3");
            resultClip = NAudioPlayer.FromMp3Data(www.bytes);
        }
        else
        {
            Debug.Log("wav");
            resultClip = www.audioClip;
        }
        audioSource.clip = resultClip;
        StartAudioImageImporter(audioPath);
        yield return null;
    }

    private void StartAudioImageImporter(string audioPath)
    {
        Debug.Log("size = " + resultClip.length);
        aii.ProcessAudioFile(audioPath, resultClip.length);
    }

    //deze wegdoen als er muziek niet meer automatisch gestart moet worden
    void Update()
    {
        if (audioSource.clip != null) {
            if (!audioSource.isPlaying && audioSource.clip.isReadyToPlay)
                audioSource.Play();
        }
    }
}
