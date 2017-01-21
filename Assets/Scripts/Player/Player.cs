using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float respawnDelay = 5f;
    [SerializeField]
    private GameObject playerDeathParticleSystem;

    public void OnBecameInvisible()
    {
        GameObject particles = Instantiate(playerDeathParticleSystem, gameObject.transform);
        particles.transform.localPosition = Vector3.zero;
        Vector2 LookAtPoint = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        particles.transform.LookAt(LookAtPoint);

        StartCoroutine("Respawn", respawnDelay);
    }

    public void ShowPlayer(bool show)
    {
        this.GetComponent<MeshRenderer>().enabled = show;
        this.GetComponent<CircleCollider2D>().enabled = show;
    }

    IEnumerator Respawn(float spawnDelay)
    {
        ShowPlayer(false);

        yield return new WaitForSeconds(spawnDelay);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 newPos = new Vector3(camera.transform.position.x, 
                                     camera.transform.position.y, transform.position.z);
        gameObject.transform.position = newPos;
        ShowPlayer(true);
    }
}
