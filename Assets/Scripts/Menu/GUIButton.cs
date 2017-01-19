using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* List of buttons to be placed relative to the screen size. */
public abstract class GUIButton : GUIBlock {
    public string label = "Button";
    public Texture image = null;

    protected abstract void DoAction();

    // Draw the button.
    protected override void DrawContent(int x, int y, int blockWidth, int blockHeight)
    {
        Rect rectangle = new Rect(x - blockWidth / 2, y - blockHeight / 2,
                                    blockWidth, blockHeight);
        if (image != null)
        {
            if (GUI.Button(rectangle, image, style.button))
                DoAction();
        } else
        {
            if (GUI.Button(rectangle, label, style.button))
                DoAction();
        }
    }
}
