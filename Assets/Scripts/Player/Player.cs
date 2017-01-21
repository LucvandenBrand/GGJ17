﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Player behavior. */
public class Player : MonoBehaviour
{
    [SerializeField]
    private float respawnDelay = 5f;
    [SerializeField]
    private GameObject playerDeathParticleSystem;
    [SerializeField]
    private int lives = 5;

    public void OnBecameInvisible()
    {
        kill();
    }

    public void kill()
    {
        lives = Mathf.Max(0, lives - 1);
        GameObject particles = Instantiate(playerDeathParticleSystem, Camera.main.transform);
        particles.transform.position = gameObject.transform.position;
        Vector2 LookAtPoint = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        particles.transform.LookAt(LookAtPoint);
        string particle = lives.ToString();
        if (lives < 1)
            particle = "skull";
        else
            StartCoroutine("Respawn", respawnDelay);
        particles.GetComponent<ParticleSystemRenderer>().material.mainTexture = Resources.Load("Textures/Lives/" + particle) as Texture;
        ShowPlayer(false);
    }

    public void ShowPlayer(bool show)
    {
        this.GetComponent<MeshRenderer>().enabled = show;
        foreach (MeshRenderer renderer in this.GetComponentsInChildren<MeshRenderer>())
            renderer.enabled = show;
        this.GetComponent<CircleCollider2D>().enabled = show;
    }

    IEnumerator Respawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 newPos = new Vector3(camera.transform.position.x, 
                                     camera.transform.position.y, transform.position.z);
        gameObject.transform.position = newPos;
        ShowPlayer(true);
    }
}
