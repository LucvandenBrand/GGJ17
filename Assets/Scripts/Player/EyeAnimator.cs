using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class EyeAnimator : MonoBehaviour {
    private Control control;

    public void Start()
    {
        this.control = GetComponent<Control>();
    }

    public void Update()
    {
        PointAt(control.GetDirection());
    }

    public void PointAt(Vector2 direction)
    {
        float tiltY = 90 - direction.x*45;
        float tiltZ = direction.y*45;
        Quaternion target = Quaternion.Euler(0, tiltY, tiltZ);
        gameObject.transform.rotation = target;
    }
}
