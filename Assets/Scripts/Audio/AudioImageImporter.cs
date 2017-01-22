using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Application = UnityEngine.Application;

public class AudioImageImporter : MonoBehaviour {
    private string audioFileName;
    private float audioLength;
    [SerializeField] private int samplesPerSecond;
    [SerializeField] private float intensityDecreaser;
    [SerializeField] private float intensityMultiplier;
    Texture2D freqArr;

    public void ProcessAudioFile(string audioFileName, float audioLength)
    {
        this.audioFileName = audioFileName;
        this.audioLength = audioLength;
        UnityEngine.Debug.Log("AudiofileName: " + audioFileName+ "length= "+ audioLength);
        CreateAudioPNG();
        LoadAudioPNG();
    }

    private void CreateAudioPNG()
    {
        //UnityEngine.Debug.Log("Starting analysis");
        Process process = new Process();

        /*if (Application.platform == RuntimePlatform.WindowsEditor)
            process.StartInfo.FileName = @"sox\sox.exe";
        else*/
#if UNITY_EDITOR
        process.StartInfo.FileName = @"sox\sox.exe";
#else
        process.StartInfo.FileName = @"EyeCantHear_Data\sox\sox.exe";
#endif
        process.StartInfo.WorkingDirectory = Application.dataPath;
        process.StartInfo.Arguments = "\""+ audioFileName + "\" -n spectrogram -r -m -x " + SampleFromTime(audioLength);
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
        process.StartInfo.UseShellExecute = false;
        //process.StartInfo.CreateNoWindow = true;
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

    private int SampleFromTime(float timeInS)
    {
        return (int)(timeInS * samplesPerSecond);
    }

    private void LoadAudioPNG()
    {
        byte[] image;
#if UNITY_EDITOR
        image = File.ReadAllBytes(@"Assets\spectrogram.png");
#else
        image = File.ReadAllBytes(@"EyeCantHear_Data\spectrogram.png");
#endif
        freqArr = new Texture2D(2, 2);
        freqArr.LoadImage(image);
        //UnityEngine.Debug.Log(freqArr.GetPixels(0, 0, 1, freqArr.height).Length);
    }

    public float GetIntensity(float time)
    {
        if (freqArr == null) 
            LoadAudioPNG();
        
        float result = 0;
        Color[] colum = freqArr.GetPixels(SampleFromTime(time), 0, 1, freqArr.height);
        for (int i=0; i < colum.Length; i++)
            result += colum[i].grayscale;
        
        //UnityEngine.Debug.Log(result / colum.Length);

        float intensity = (result/colum.Length - intensityDecreaser);
        if (intensity < 0)
            intensity = 0;

        return intensity * intensityMultiplier;
    }

    public float GetBaseIntensity(float time)
    {
        if (freqArr == null) 
            LoadAudioPNG();
        
        float result = 0;
        Color[] colum = freqArr.GetPixels( SampleFromTime(time), 0, 1, freqArr.height);

        for (int i=0; i < colum.Length / 2; i++)
            result += colum[i].grayscale - colum[(colum.Length / 2) + i].grayscale;
        
        //UnityEngine.Debug.Log(result / colum.Length);

        float intensity = (result/colum.Length - intensityDecreaser);
        if (intensity < 0)
            intensity = 0;

        return intensity * intensityMultiplier;
    }
}
