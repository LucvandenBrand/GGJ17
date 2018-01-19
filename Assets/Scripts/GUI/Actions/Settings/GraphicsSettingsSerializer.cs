using UnityEngine;

/* Class that allows for storing and loading custom graphics
 * settings made by the user. It loads these settings 
 * on starting the script. */
public class GraphicsSettingsSerializer : MonoBehaviour
{
    private static string GRAPHICS_SETGRAPHICS = "graphics_setgraphics";
    private static string GRAPHICS_FULLSCREEN = "graphics_fullscreen";
    private static string GRAPHICS_RESOLUTION = "graphics_resolution";
    private static string GRAPHICS_QUALITY = "graphics_quality";

    /* When this script is created, recover all previously stored settings. */
    public void Start()
    {
        LoadSettings();
    }

    /* Load and set all previously stored settings. */
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(GRAPHICS_SETGRAPHICS))
        {
            SetFullScreen(IntegerToBool(PlayerPrefs.GetInt(GRAPHICS_FULLSCREEN)));
            SetResolution(PlayerPrefs.GetInt(GRAPHICS_RESOLUTION));
            SetGraphicsQuality(PlayerPrefs.GetInt(GRAPHICS_QUALITY));
        }
    }

    /* Save all settings on the disk. */
    public void SaveSettings()
    {
        PlayerPrefs.SetInt(GRAPHICS_FULLSCREEN, BoolToInteger(Screen.fullScreen));
        PlayerPrefs.SetInt(GRAPHICS_QUALITY, QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt(GRAPHICS_RESOLUTION, GetIndexOfResolution(Screen.currentResolution));
        PlayerPrefs.SetInt(GRAPHICS_SETGRAPHICS, BoolToInteger(true));
        PlayerPrefs.Save();
    }

    /* Search the list of resolution options and return the resolution
     * index that matches the provided resolution. */
    private int GetIndexOfResolution(Resolution resolution)
    {
        int index = 0;
        foreach (Resolution resolutionOption in Screen.resolutions)
        {
            if (resolution.Equals(resolution))
                return index;
            index++;
        }
        throw new UnityException("Resolution not in the list of options.");
    }

    private void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        SetResolution(resolution.width, resolution.height, resolution.refreshRate);
    }

	private void SetResolution(int width, int height, int refreshRate)
    {
        Screen.SetResolution(width, height, Screen.fullScreen, refreshRate);
    }

    private void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /* Convert integer type to bool type, to work with playerprefs. */
    private int BoolToInteger(bool boolean)
    {
        if (boolean)
            return 1;
        return 0;
    }

    private bool IntegerToBool(int integer)
    {
        return integer == 1;
    }
}
