using UnityEngine;
using System.Collections;

public class Spawner : AudioImpactListener {
    [SerializeField]
    private GameObject[] ObjectToSpawn;
    [SerializeField]
    private float length = 25;
    [SerializeField]
    private float spawnThreshold = 10;
    private float intensitySum = 0;
    [SerializeField]
    private float enemySpeed = 0.2f;

    public override void AudioImpact(float speed, float intensity)
    {
        intensitySum += intensity;
        if (intensitySum > spawnThreshold)
        {
            intensitySum = 0;
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        Vector2 cameraVector2D = new Vector2(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y);
        Vector2 spawnPosition = cameraVector2D + Random.insideUnitCircle.normalized * length;
        GameObject go = Instantiate( ObjectToSpawn[ Random.Range( 0, ObjectToSpawn.Length )], spawnPosition, Quaternion.identity ) as GameObject;
        go.transform.SetParent(this.transform);

        //go.AddComponent<DestroyAfter>().destroyAfter = 5;
        Enemy enemy = go.GetComponent<Enemy>();

        enemy.movedirection = -spawnPosition.normalized;
        enemy.speed = enemySpeed;
    }
}