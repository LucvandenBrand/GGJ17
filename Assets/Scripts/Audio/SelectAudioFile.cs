using System.Collections;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using Application = UnityEngine.Application;


[RequireComponent( typeof(AudioSource) )]
public class SelectAudioFile : MonoBehaviour {
    private AudioSource audioSource;
    public Canvas loadScreen; 
    private AudioClip resultClip;



    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        SimpleFileBrowser.SetFilters(".mp3", ".wav", ".ogg", ".aiff", ".flac");
        SimpleFileBrowser.SetDefaultFilter(".mp3");
        SimpleFileBrowser.AddQuickLink(null, "Users", "C:\\Users");
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine() {
        yield return StartCoroutine(SimpleFileBrowser.WaitForLoadDialog(false, null, "Load File", "Load"));
        Instantiate(loadScreen);
        LoadAudioFile(SimpleFileBrowser.Result);
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


        // resultClip = (audioPath.EndsWith(".mp3")) ? NAudioPlayer.FromMp3Data(www.bytes) : www.audioClip;
        audioSource.clip = resultClip = NAudioPlayer.FromMp3Data( www.bytes ); //resultClip;
        yield return null;
    }


    //deze wegdoen als er muziek niet meer automatisch gestart moet worden
    void Update() {
        if (audioSource.clip != null) {
            if (!audioSource.isPlaying && audioSource.clip.isReadyToPlay)
                audioSource.Play();
        }
    }
}
