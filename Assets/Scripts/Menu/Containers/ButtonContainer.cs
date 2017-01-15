using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* List of buttons to be placed relative to the screen size. */
public abstract class ButtonContainer : Container {
    [Range(0.0f, 1.0f)]
    public float buttonHeight = 0.8f;

    private List<Button> buttons;

    public void Start()
    {
        this.buttons = new List<Button>();
        SetupButtons();
    }

    protected abstract void SetupButtons();

    // Draw the buttons.
    protected override void DrawContent(int x, int y, int blockWidth, int blockHeight)
    {
        int fullButtonPixelHeight = (int)(blockHeight / buttons.Count);
        int buttonPixelHeight = (int)(fullButtonPixelHeight * buttonHeight);

        int buttonIndex = 0;
        foreach (Button button in buttons)
        {
            Rect rectangle = new Rect(x - blockWidth / 2, y + fullButtonPixelHeight * buttonIndex - blockHeight / 2,
                                      blockWidth, buttonPixelHeight);
            if (button.GetTexture() != null)
            {
                if (GUI.Button(rectangle, button.GetTexture()))
                    button.DoAction();
            } else
            {
                if (GUI.Button(rectangle, button.GetLabel()))
                    button.DoAction();
            }
            buttonIndex++;
        }
    }

    protected void AddButton(Button button)
    {
        this.buttons.Add(button);
    }
}
