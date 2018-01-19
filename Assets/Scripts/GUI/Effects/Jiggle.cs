using UnityEngine;

/* Will 'jiggle' the UI element this script is attached to. */
[RequireComponent(typeof(RectTransform))]
public class Jiggle : MonoBehaviour
{
    [SerializeField]
    private float speedX = 10;
    [SerializeField]
    private float speedY = 10;
    [SerializeField]
    private float amplitudeX = 10;
    [SerializeField]
    private float amplitudeY = 10;
    [SerializeField]
    private float phaseX = 0;
    [SerializeField]
    private float phaseY = 0;

    private RectTransform rectTransform;
    private Vector3 origin;
    private float omegaX = 0;
    private float omegaY = 0;

	void Start ()
    {
        this.rectTransform = gameObject.GetComponent<RectTransform>();
        this.origin = rectTransform.localPosition;
	}
	
	/* Place the transform relative to the time passed and
     * set parameters. */
	void Update ()
    {
        this.omegaX += Time.deltaTime * speedX;
        this.omegaY += Time.deltaTime * speedY;
        this.omegaX %= Mathf.PI * 2;
        this.omegaY %= Mathf.PI * 2;
        float moveX = amplitudeX * Mathf.Cos(omegaX+phaseX);
        float moveY = amplitudeY * Mathf.Sin(omegaY+phaseY);
        rectTransform.localPosition = origin + new Vector3(moveX, moveY);
	}
}
