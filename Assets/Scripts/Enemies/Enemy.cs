using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [HideInInspector]
	public Vector3 movedirection;
    [HideInInspector]
    public float speed;


    void Update() {
        transform.Translate( movedirection * speed );
    }
}
