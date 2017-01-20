using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class audioImageImporter : MonoBehaviour {
    [SerializeField]
    private string audioFileName;

	// Use this for initialization
	void Start () {
        CreateAudioPNG();
    }

    private void CreateAudioPNG()
    {
        UnityEngine.Debug.Log("Starting analysis");
        Process process = new Process();

        process.StartInfo.FileName = "sox";
        process.StartInfo.Arguments = audioFileName + @" -n spectrogram -r -m -x 2700";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.Start();

        UnityEngine.Debug.Log("Starting Done");

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


        //process.WaitForExit();
        UnityEngine.Debug.Log(standardOutput);
        UnityEngine.Debug.Log(standardError);
        UnityEngine.Debug.Log("Everything Done");
    }

    void LoadAudioPNG()
    {
        byte[] image = System.IO.File.ReadAllBytes("spectrogram.png");
        Texture2D freqArr = new Texture2D(2, 2);
        freqArr.LoadImage(image);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
