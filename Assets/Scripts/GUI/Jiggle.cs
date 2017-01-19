using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Jiggle : MonoBehaviour {
    public float speedX = 10;
    public float speedY = 10;
    public float amplitudeX = 10;
    public float amplitudeY = 10;
    private RectTransform rectTransform;
    private Vector3 origin;
    private float phaseX = 0;
    private float phaseY = 0;

	// Use this for initialization
	void Start () {
        this.rectTransform = gameObject.GetComponent<RectTransform>();
        this.origin = rectTransform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        this.phaseX += Time.deltaTime * speedX;
        this.phaseY += Time.deltaTime * speedY;
        float moveX =  amplitudeX * Mathf.Cos(phaseX);
        float moveY = amplitudeY * Mathf.Sin(phaseY);
        rectTransform.localPosition = origin + new Vector3(moveX, moveY);
	}
}
