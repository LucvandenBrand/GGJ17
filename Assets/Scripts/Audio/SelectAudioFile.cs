using System.Collections;
using UnityEngine;
using System.Diagnostics;
using System.IO;

/* This class opens a file browser that prompts for an audio file.
 * The audio file is downloaded into the assets and played on the
 * (required) attached AudioSource. */
[RequireComponent( typeof(AudioSource) )]
public class SelectAudioFile : MonoBehaviour {
    [SerializeField]
    private Canvas loadScreen;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SimpleFileBrowser.SetFilters(".mp3", ".wav", ".ogg", ".aiff", ".flac");
        SimpleFileBrowser.SetDefaultFilter("All Files");
        SimpleFileBrowser.AddQuickLink(null, "Users", "C:\\Users");
        StartCoroutine(ShowLoadDialog());
    }

    void Update()
    {
        if (audioSource.clip != null)
            if (!audioSource.isPlaying && audioSource.clip.loadState == AudioDataLoadState.Loaded)
                audioSource.Play();
    }

    IEnumerator ShowLoadDialog()
    {
        yield return StartCoroutine(SimpleFileBrowser.WaitForLoadDialog(false, null, "Load File", "Load"));
        Instantiate(loadScreen);
        Application.runInBackground = true;
        yield return StartCoroutine(LoadAudio(SimpleFileBrowser.Result));
        Application.runInBackground = false;
    }

    /* Download the file to the assets and insert it into the AudioSouce. */
    IEnumerator LoadAudio(string audioPath)
    {
        if (!audioPath.EndsWith(".wav"))
            audioPath = convertSoundToWav(audioPath);
        string url = "file:///" + audioPath;
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        audioSource.clip = www.audioClip;
        yield return null;
    }

    /* As we can only stream wav files, this function can convert files to that format. */
    private string convertSoundToWav(string audioPath)
    {
        string tmpWavPath =Path.Combine(Application.dataPath, "tmp.wav");

        Process process = new Process();
        process.StartInfo.FileName = SoxFilename();
        process.StartInfo.WorkingDirectory = Application.dataPath;
        process.StartInfo.Arguments = "\"" + audioPath + "\" " + tmpWavPath;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.Start();

        var standardOutput = new System.Text.StringBuilder();
        var standardError = new System.Text.StringBuilder();

        // read chunk-wise while process is running.
        while (!process.HasExited)
        {
            standardOutput.Append(process.StandardOutput.ReadToEnd());
            standardError.Append(process.StandardError.ReadToEnd());
        }

        // make sure not to miss out on any remaindings.
        standardOutput.Append(process.StandardOutput.ReadToEnd());
        standardError.Append(process.StandardError.ReadToEnd());

        if (standardError.ToString() != "")
        {
            UnityEngine.Debug.Log(standardError);
        }
        return tmpWavPath;
    }

    /* Get the proper filename for sox, as it is OS dependent. */
    private string SoxFilename()
    {
        switch (Application.platform)
        {
            case (RuntimePlatform.LinuxEditor):
                return Path.Combine("sox", "sox_linux");
            case (RuntimePlatform.WindowsEditor):
                return Path.Combine("sox", "sox.exe");
            case (RuntimePlatform.LinuxPlayer):
                return Path.Combine("EyeCantHear_Data", Path.Combine("sox", "sox_linux"));
            case (RuntimePlatform.WindowsPlayer):
                return Path.Combine("EyeCantHear_Data", Path.Combine("sox", "sox.exe"));
            default:
                UnityEngine.Debug.LogError("RuntimePlatform " + Application.platform + " unsupported!");
                Application.Quit();
                return "";
        }
    }
}
