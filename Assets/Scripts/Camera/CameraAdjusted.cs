using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjusted : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        ScaleMesh();
	}

    // Scales the mesh to perfectly fit the screen.
    void ScaleMesh()
    {
        float screenX = Screen.width;
        float screenY = Screen.height;
        float height = Camera.main.orthographicSize * 2;
        float width = height * screenX / screenY;
        transform.localScale = new Vector3(width, height, 1);
    }
}