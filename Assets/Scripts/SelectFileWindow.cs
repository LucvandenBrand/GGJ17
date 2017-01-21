using System.Runtime.InteropServices;
using UnityEngine;

public class SelectFileWindow
{

    [DllImport("user32.dll")]
    private static extern void OpenFileDialog(); //in your case : OpenFileDialog

    public static string OpenWindow()
    {
        System.Windows.Forms.OpenFileDialog sfd = new System.Windows.Forms.OpenFileDialog();
        sfd.Title = "Select a song.";
        sfd.ShowDialog();
        Debug.Log(sfd.FileName);
        return sfd.FileName;
    }
}
