using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A GUI item item that can be placed and scaled relative to the screen. */
public abstract class Container : MonoBehaviour {
    [Range(0.0f, 1.0f)]
    public float width = 0.5f;
    [Range(0.0f, 1.0f)]
    public float height = 0.5f;
    [Range(0.0f, 1.0f)]
    public float x = 0.5f;
    [Range(0.0f, 1.0f)]
    public float y = 0.5f;

    // Calculate the content's relative position.
    public void OnGUI()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        int blockX = (int)(screenWidth * x);
        int blockY = (int)(screenHeight * y);
        int blockWidth = (int)(screenWidth * width);
        int blockHeight = (int)(screenHeight * height);
        DrawContent(blockX, blockY, blockWidth, blockHeight);
    }

    // Show the content.
    protected abstract void DrawContent(int x, int y, int blockWidth, int blockHeight);
}
