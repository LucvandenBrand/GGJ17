using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectAudioFile : MonoBehaviour {
    public AudioSource audioSource;
    public AudioImageImporter aii;
    private AudioClip resultClip;
    // Use this for initialization
    void Start() {
        SelectAudio();
    }

    public void SelectAudio() {
        string audioPath = SelectFileWindow.OpenWindow();
        if (audioPath.Equals( "" )) {
            audioPath = SelectFileWindow.OpenWindow();
        }
        LoadAudioFile(audioPath);
    }

    public void LoadAudioFile( string audioPath ) {
        StartCoroutine(LoadAudio(audioPath));

    }

    IEnumerator LoadAudio( string audioPath ) {
        string url = "file:///" + audioPath;
        WWW www = new WWW(url);
        while (!www.isDone) {
            yield return new WaitForEndOfFrame();
        }

        resultClip = (audioPath.EndsWith(".mp3")) ? NAudioPlayer.FromMp3Data(www.bytes) : www.audioClip; 
        
        audioSource.clip = resultClip;
        StartAudioImageImporter(audioPath);
        yield return null;
    }

    private void StartAudioImageImporter( string audioPath ) {
        Debug.Log("size = " + resultClip.length);
        aii.ProcessAudioFile(audioPath, resultClip.length);
    }

    //deze wegdoen als er muziek niet meer automatisch gestart moet worden
    void Update() {
        if (audioSource.clip != null) {
            if (!audioSource.isPlaying && audioSource.clip.isReadyToPlay)
                audioSource.Play();
        }
    }
}
