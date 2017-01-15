using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIImage : GUIBlock {
    public Texture image;

    protected override void DrawContent(int x, int y, int blockWidth, int blockHeight)
    {
        if (blockWidth == 0 && blockHeight != 0)
            blockWidth = (int) (1.0 * blockHeight / image.height * image.width);
        else if (blockHeight == 0 && blockWidth != 0)
            blockHeight = (int)(1.0 * blockWidth / image.width * image.height);
        Rect rectangle = new Rect(x-blockWidth/2, y-blockHeight/2, blockWidth, blockHeight);
        GUI.DrawTexture(rectangle, image);
    }
}
