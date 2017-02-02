using UnityEngine;
using System.Collections;

/* Destroys the GameObject after a set amount of time. */
public class DestroyAfter : MonoBehaviour {
    [SerializeField]
    private float destroyDelay = 1f;
    private float startTime;

    public void Start() {
        startTime = Time.time;
    }

    public void Update() {
        if (startTime + destroyDelay <= Time.time) {
            Destroy(this.gameObject);
        }
    }

    public void SetDestroyDelay(float destroyDelay)
    {
        this.destroyDelay = destroyDelay;
    }
}
