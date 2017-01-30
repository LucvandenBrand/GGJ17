using UnityEngine;

/* A camera that always moves in one given direction, 
 * based on the music currently playing. */
[RequireComponent(typeof(Camera))]
public class MovingCamera : AudioImpactListener {

    [SerializeField]
    private float intensitySumThreshold = 5;
    [SerializeField]
    private float acceleration = 1;

    private Camera moveCamera;
    private FloatAverage average = new FloatAverage(15);
    private float direction = 0;
    private float intensitySum = 0;

	public void Start ()
    {
        base.Start();
        this.moveCamera = GetComponent<Camera>();
	}

    /* Every audioimpact, move the camera in a certain direction. */
    public override void AudioImpact(float intensity)
    {
        UpdateDirection(intensity);
        Move( 5 * acceleration);
    }
    
    /* Change the direction if a threshold of summed intensity is met. */
    private void UpdateDirection(float intensity)
    {
        this.average.Add(intensity);
        this.intensitySum += this.average.GetAverage();
        if (this.intensitySum > this.intensitySumThreshold)
        {
            this.intensitySum = 0;
            direction = Random.Range(0, Mathf.PI * 2);
        }
    }

    private void Move(float speed)
    {
        float xUnit = Mathf.Cos(direction);
        float yUnit = Mathf.Sin(direction);
        float independentSpeed = speed * Time.deltaTime;
        float xMove = xUnit * independentSpeed;
        float yMove = yUnit * independentSpeed;
        Vector3 movement = new Vector3(xMove, yMove);
        moveCamera.transform.position += movement;
    }
}
