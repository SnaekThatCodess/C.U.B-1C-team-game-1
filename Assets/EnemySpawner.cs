using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public float MinSpawnRate = 2f;
    public float MaxSpawnRate = 10f;
    public float RotationSpeed = 20f;
    public float SpawnAreaSize = 1f;
    public Transform pivot;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    private IEnumerator SpawnEnemies()
    {
        float spawnInterval = Random.Range(MinSpawnRate, MaxSpawnRate);
        yield return new WaitForSeconds(spawnInterval);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, EnemyPrefabs.Length);
        GameObject enemyToSpawn = EnemyPrefabs[enemyIndex];
        float offsetX = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        float offsetY = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0);
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, RotationSpeed * Time.deltaTime);
    }
}