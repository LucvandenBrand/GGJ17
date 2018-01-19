using UnityEngine;

/* Animates the object to point to the direction the 
 * Control script is moving it. */
[RequireComponent(typeof(Control))]
public class EyeAnimator : MonoBehaviour
{
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
        float tiltY = 90 - direction.x * 45;
        float tiltZ = direction.y * 45;
        Quaternion currentRotation = gameObject.transform.rotation;
        Quaternion target = Quaternion.Euler(0, tiltY, tiltZ);
        
        gameObject.transform.rotation = Quaternion.Lerp(currentRotation, target, 0.1f);
    }
}
