using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A camera that always moves in one given direction. */
[RequireComponent(typeof(Camera))]
public class MovingCamera : AudioImpactListener {
    [Range(0.0f, 1.0f)]
    public float sensitivity = 0.5f;
    private Camera moveCamera;
    private FloatAverage average = new FloatAverage(15);
    private float direction = 0;

	// Use this for initialization
	void Start () {
        this.moveCamera = GetComponent<Camera>();
	}

    public override void AudioImpact(float speed, float intensity)
    {
        UpdateDirection(intensity);
        Move(speed);
    }

    private void UpdateDirection(float intensity)
    {
        this.average.Add(intensity);
        direction += average.GetAverage()*sensitivity;
        direction %= (Mathf.PI * 2);
    }

    private void Move(float speed)
    {
        float xUnit = Mathf.Cos(direction);
        float yUnit = Mathf.Sin(direction);
        float independentSpeed = speed * Time.deltaTime;
        float xMove = xUnit * independentSpeed;
        float yMove = yUnit * independentSpeed;
        Vector3 movement = new Vector3(xMove, 0, yMove);
        moveCamera.transform.position += movement;
    }
}
