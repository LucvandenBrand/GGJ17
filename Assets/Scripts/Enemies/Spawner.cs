using UnityEngine;
using System.Collections;

/* Spawns enemies added to its spawn lists bases on the music currently playing. */
public class Spawner : AudioImpactListener {
    [SerializeField]
    private GameObject[] ObjectToSpawn;
    [SerializeField]
    private float length = 25;
    [SerializeField]
    private float spawnThreshold = 10;
    [SerializeField]
    private float enemySpeed = 0.2f;
    private float intensitySum = 0;

    public override void AudioImpact(float intensity)
    {
        intensitySum += intensity;
        if (intensitySum > spawnThreshold)
        {
            intensitySum = 0;
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        int maxSpawnableObjectTypeIndex = 0;
        try{
           maxSpawnableObjectTypeIndex = Mathf.CeilToInt((GetAudioSystemController().GetAudioSource().time / 
                                                          GetAudioSystemController().GetAudioSource().clip.length) * 
                                                          ObjectToSpawn.Length);
        } catch (System.NullReferenceException e) {
            Debug.LogError(e);
        }

        Vector2 cameraVector2D = new Vector2(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y);
        Vector2 spawnPosition = cameraVector2D + Random.insideUnitCircle.normalized * length;
        GameObject go = Instantiate( ObjectToSpawn[ Random.Range( 0, maxSpawnableObjectTypeIndex)], spawnPosition, Quaternion.FromToRotation(spawnPosition.normalized, Vector3.zero), this.transform ) as GameObject;
    }
}
