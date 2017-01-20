using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class audioImageImporter : MonoBehaviour {
    [SerializeField] private string audioFileName;
    [SerializeField] private float audioLength;
    [SerializeField] private int samplesPerSecond;
    private int columCntr = 0;
    Texture2D freqArr;

    // Use this for initialization
    void Start () {
        CreateAudioPNG();
        LoadAudioPNG();
    }

    private void CreateAudioPNG()
    {
        //UnityEngine.Debug.Log("Starting analysis");
        Process process = new Process();

        process.StartInfo.FileName = "sox";
        process.StartInfo.Arguments = audioFileName + @" -n spectrogram -r -m -x " + SampleFromTime(audioLength);
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.Start();

        //UnityEngine.Debug.Log("Starting Done");

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
        //UnityEngine.Debug.Log(standardOutput);
        if (standardError.ToString() != "")
        {
            UnityEngine.Debug.Log(standardError);
        }
        
        //UnityEngine.Debug.Log("Everything Done");
    }

    int SampleFromTime(float timeInS)
    {
        return (int)(timeInS * samplesPerSecond);
    }

    void LoadAudioPNG()
    {
        byte[] image = System.IO.File.ReadAllBytes("spectrogram.png");
        freqArr = new Texture2D(2, 2);
        freqArr.LoadImage(image);
        //UnityEngine.Debug.Log(freqArr.GetPixels(0, 0, 1, freqArr.height).Length);
    }

    float GetIntensity(float time)
    {
        float result = 0;
        Color[] colum = freqArr.GetPixels(SampleFromTime(time), 0, 1, freqArr.height);
        for (int i=0; i<colum.Length; i++)
        {
            result += colum[i].grayscale;
        }
        return result/colum.Length;
    }
}
