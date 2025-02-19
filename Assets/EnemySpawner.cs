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
    public GameManager gameManager;
    public GameManager2 gameManager2;
    public GameManager3 gameManager3;
    public GameManager4 gameManager4;
    public GameManager5 gameManager5;
    public GameManager6 gameManager6;
    public GameManager7 gameManager7;
    public GameManager8 gameManager8;
    public GameManager9 gameManager9;
    public GameManager10 gameManager10;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float spawnInterval = Random.Range(MinSpawnRate, MaxSpawnRate);
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    { 
        int enemyIndex = Random.Range(0, EnemyPrefabs.Length);
        GameObject enemyToSpawn = EnemyPrefabs[enemyIndex];
        float offsetX = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        float offsetY = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0);
        GameObject enemyObject = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        Debug.Log("Spawned enemy: " + enemyObject.name);

        if (enemyObject.GetComponent<NormalEnemyScript>())
        {
            gameManager.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager2.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager3.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager4.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager5.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager6.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager7.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager8.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager9.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
            gameManager10.NormalEnemies.Add(enemyObject.GetComponent<NormalEnemyScript>());
        }
        else if (enemyObject.GetComponent<TankEnemyScript>())
        {
            gameManager.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager2.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager3.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager4.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager5.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager6.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager7.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager8.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager9.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
            gameManager10.TankEnemies.Add(enemyObject.GetComponent<TankEnemyScript>());
        }
        else if (enemyObject.GetComponent<StalkerEnemyScript>())
        {
            gameManager.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager2.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager3.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager4.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager5.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager6.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager7.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager8.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager9.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
            gameManager10.StalkerEnemies.Add(enemyObject.GetComponent<StalkerEnemyScript>());
        }
    }

    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, RotationSpeed * Time.deltaTime);
    }
}