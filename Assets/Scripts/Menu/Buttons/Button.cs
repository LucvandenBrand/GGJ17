using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A button is a class that can be added to the menu.*/

public abstract class Button {
    private string label;
    private Texture texture;

	public Button(string label)
    {
        this.label = label;
        this.texture = null;
    }

    /* The action to be done when clicked. */
    public abstract void DoAction();

    public void SetTexture(Texture texture)
    {
        this.texture = texture;
    }
	
	public string GetLabel()
    {
        return this.label;
    }

    public Texture GetTexture()
    {
        return this.texture;
    }
}
