using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureContainer : Container {
    public Texture texture;

    protected override void DrawContent(int x, int y, int blockWidth, int blockHeight)
    {
        if (blockWidth == 0 && blockHeight != 0)
            blockWidth = (int) (1.0 * blockHeight / texture.height * texture.width);
        else if (blockHeight == 0 && blockWidth != 0)
            blockHeight = (int)(1.0 * blockWidth / texture.width * texture.height);
        Rect rectangle = new Rect(x-blockWidth/2, y-blockHeight/2, blockWidth, blockHeight);
        GUI.DrawTexture(rectangle, texture);
    }
}
