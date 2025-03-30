using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject barrelPrefab;
    public float spawnInterval = 3f;
    public float spawnIntervalVariance = 1f;
    
    private float nextSpawnTime;
    
    void Start()
    {
        // Calculate first spawn time
        CalculateNextSpawnTime();
    }
    
    void Update()
    {
        // Check if it's time to spawn a new barrel
        if (Time.time >= nextSpawnTime)
        {
            SpawnBarrel();
            CalculateNextSpawnTime();
        }
    }
    
    void SpawnBarrel()
    {
        // Instantiate a new barrel at the spawner's position
        Instantiate(barrelPrefab, transform.position, Quaternion.Euler(0, 0, 90));
    }
    
    void CalculateNextSpawnTime()
    {
        // Add some randomness to spawn interval
        float variance = Random.Range(-spawnIntervalVariance, spawnIntervalVariance);
        nextSpawnTime = Time.time + spawnInterval + variance;
    }
}