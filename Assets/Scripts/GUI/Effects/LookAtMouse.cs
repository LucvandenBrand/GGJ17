using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // speed is the rate at which the object will rotate
    [SerializeField]
    private float speed;

    void FixedUpdate()
    {

        var target = Input.mousePosition;
        target.z = 5.0f;
        target = Camera.main.ScreenToWorldPoint(target);

        // Determine the target rotation.  This is the rotation if the transform looks at the target point.
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
