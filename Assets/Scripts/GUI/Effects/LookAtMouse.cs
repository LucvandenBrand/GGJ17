using UnityEngine;

/* Makes the object orient itself to always face the cursor. */
public class LookAtMouse : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void FixedUpdate()
    {
        var target = Input.mousePosition;
        target.z = 5.0f;
        target = Camera.main.ScreenToWorldPoint(target);
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
