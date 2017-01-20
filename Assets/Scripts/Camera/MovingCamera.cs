using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A camera that always moves in one given direction. */
[RequireComponent(typeof(Camera))]
public class MovingCamera : AudioImpactListener {
    private Camera moveCamera;
    private float direction;

	// Use this for initialization
	void Start () {
        this.moveCamera = GetComponent<Camera>();
	}

    public override void audioImpact(float speed, float intensity)
    {
        float xUnit = Mathf.Cos(intensity);
        float yUnit = Mathf.Sin(intensity);
        float independentSpeed = speed * Time.deltaTime;
        float xMove = xUnit * independentSpeed;
        float yMove = yUnit * independentSpeed;
        Vector3 movement = new Vector3(xMove, 0, yMove);
        moveCamera.transform.position += movement;
    }
}
