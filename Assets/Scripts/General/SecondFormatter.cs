using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Formats the given seconds into a nice second minute format. */
public class SecondFormatter
{
    public static string FormatSeconds(int rawSeconds)
    {
        int minutes = rawSeconds / 60;
        int seconds = rawSeconds % 60;
        string secondsStr = "";
        secondsStr += minutes + ":";

        if (seconds < 10)
            secondsStr += "0";
        
        return secondsStr += seconds;
    }
}
