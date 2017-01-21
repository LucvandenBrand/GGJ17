using UnityEngine;
using System.Collections;

public class Spawner : AudioImpactListener {
    [SerializeField]
    private GameObject[] ObjectToSpawn;
    [SerializeField]
    private float length = 25;

    private Camera mainCamera;

    public override void AudioImpact(float speed, float intensity)
    {
        SpawnEnemy();
    }

    void SpawnEnemy() {
        Vector2 cameraVector2D = new Vector2(mainCamera.gameObject.transform.position.x, mainCamera.gameObject.transform.position.y);
        Vector2 spawnPosition = cameraVector2D + Random.insideUnitCircle.normalized * length;
        GameObject go = Instantiate( ObjectToSpawn[ Random.Range( 0, ObjectToSpawn.Length )], spawnPosition, Quaternion.identity ) as GameObject;
        go.transform.SetParent(this.transform);

        go.AddComponent<DestroyAfter>().destroyAfter = 5;
        Enemy enemy = go.GetComponent<Enemy>();

        enemy.movedirection = -spawnPosition.normalized;
        enemy.speed = Random.Range( 0.25f, 0.50f );
    }
}