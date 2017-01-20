using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Jiggle : MonoBehaviour {
    public float speedX = 10;
    public float speedY = 10;
    public float amplitudeX = 10;
    public float amplitudeY = 10;
    public float phaseX = 0;
    public float phaseY = 0;
    private RectTransform rectTransform;
    private Vector3 origin;
    private float omegaX = 0;
    private float omegaY = 0;

	// Use this for initialization
	void Start () {
        this.rectTransform = gameObject.GetComponent<RectTransform>();
        this.origin = rectTransform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        this.omegaX += Time.deltaTime * speedX;
        this.omegaY += Time.deltaTime * speedY;
        this.omegaX %= Mathf.PI * 2;
        this.omegaY %= Mathf.PI * 2;
        float moveX =  amplitudeX * Mathf.Cos(omegaX+phaseX);
        float moveY = amplitudeY * Mathf.Sin(omegaY+phaseY);
        rectTransform.localPosition = origin + new Vector3(moveX, moveY);
	}
}
