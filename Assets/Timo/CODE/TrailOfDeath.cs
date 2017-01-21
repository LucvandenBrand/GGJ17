using UnityEngine;
using System.Collections;

public class TrailOfDeath : MonoBehaviour {

    [SerializeField] private GameObject fire;
    [SerializeField] private float fireRate = 1f;



    void Start() {
        StartCoroutine( run() );
    }

    IEnumerator run () {
        while (true) {
            yield return new WaitForSeconds( fireRate );
            Instantiate( fire, transform.position, Quaternion.identity );
        }

        yield return null;
	}
}
