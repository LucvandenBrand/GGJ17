using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    Vector3 movementVector = new Vector3( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0 );
        transform.Translate( movementVector ); 
	}
}
