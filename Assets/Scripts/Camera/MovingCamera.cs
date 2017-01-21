using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* A camera that always moves in one given direction. */
[RequireComponent(typeof(Camera))]
public class MovingCamera : AudioImpactListener {

    [SerializeField]
    private float intensitySumThreshold = 5;
    private Camera moveCamera;
    private FloatAverage average = new FloatAverage(15);
    private float direction = 0;
    private float intensitySum = 0;
    [SerializeField]
    private float acceleration = 1;

	// Use this for initialization
	public void Start () {
        base.Start();
        this.moveCamera = GetComponent<Camera>();
	}

    public override void AudioImpact(float speed, float intensity)
    {
        UpdateDirection(intensity);
        Move(speed*acceleration);
    }

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
