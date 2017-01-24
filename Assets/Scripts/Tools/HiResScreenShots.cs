using UnityEngine;
using System.Collections;
using System.IO;

public class HiResScreenShots : MonoBehaviour {

    public int resWidth = 1920; public int resHeight = 1080;

    [Range(1, 6)]
    public int enlarge = 1;

    public bool  dontDestoryOnLoad= true;
    public string screenshotFolderPath = "/../screenshots/";
    public bool  startPathFromProjectPath = true;
    public KeyCode screenshotKey = KeyCode.F8;



    void Start() {
        if (dontDestoryOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    public string ScreenShotName( int width, int height ) {
        return string.Format("{0}/screen_{1}x{2}_{3}.png",
                             ((startPathFromProjectPath)? Application.dataPath : "" ) + screenshotFolderPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }


    void LateUpdate() {
        if (Input.GetKeyDown(screenshotKey)) {
#if UNITY_EDITOR || UNITY_EDITOR_64
            RenderTexture rt = new RenderTexture(resWidth * enlarge, resHeight * enlarge, 24);
            Camera.main.targetTexture = rt;

            Texture2D screenShot = new Texture2D(resWidth * enlarge, resHeight * enlarge, TextureFormat.RGB24, false);
            Camera.main.Render();
            RenderTexture.active = rt;

            screenShot.ReadPixels(new Rect(0, 0, resWidth * enlarge, resHeight * enlarge), 0, 0);
            Camera.main.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);

            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth * enlarge, resHeight * enlarge);
            string path = ((startPathFromProjectPath)? Application.dataPath : "" ) + screenshotFolderPath;

            if (Directory.Exists(path) == false) 
                Directory.CreateDirectory(path);
            
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
#endif
        }
    }
}