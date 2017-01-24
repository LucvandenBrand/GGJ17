using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float destroyAfter = 1f;
    private float startTime;


    void Start() {
        startTime = Time.time;
    }

    void Update() {
        if (startTime + destroyAfter <= Time.time) {
            Destroy(this.gameObject);
        }
    }


    public void SetDetroyTimer( float startTime ) {
        this.startTime = startTime;
    }
}
