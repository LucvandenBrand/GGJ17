using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasRenderer))]
public class OnlyWithPlayers : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playerCount == 0)
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(0); 
        else
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
    }
}
