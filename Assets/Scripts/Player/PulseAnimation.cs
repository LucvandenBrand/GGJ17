using UnityEngine;

/* Makes an object `pulse' by oscillating the scale
 *  until the script is removed. */
public class PulseAnimation : MonoBehaviour {
    [SerializeField]
    private float pulseSize = 1;
    [SerializeField]
    private float pulseSpeed = 1;
    [SerializeField]
    private float minScale = 0;

    private float alpha = 0;
    private Vector3 originalScale;

    void Start()
    {
        this.originalScale = transform.localScale;
    }
	
    /* Every frame, update alpha with the passed time
     * and set the scale to cos(alpha). */
	void Update ()
    {
        alpha += Time.deltaTime*pulseSpeed;
        alpha %= Mathf.PI * 2;
        float scale = minScale + Mathf.Cos(alpha) * pulseSize;
        Vector3 newScale = new Vector3(scale, scale, scale);
        newScale.Scale(originalScale);
        transform.localScale = newScale;
	}

    public void SetAmplitude(float amplitude)
    {
        this.pulseSize = amplitude;
    }

    public void SetFrequency(float frequency)
    {
        this.pulseSpeed = frequency;
    }

    public void SetMinScale(float scale)
    {
        this.minScale = scale;
    }

    private void OnDestroy()
    {
        transform.localScale = originalScale;
    }
}
