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

    public NormalEnemyScript NormalEnemy;
    public TankEnemyScript TankEnemy;
    public StalkerEnemyScript StalkerEnemy;

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
            SpawnNormalEnemy();
            SpawnTankEnemy();
            SpawnStalkerEnemy();

            //SpawnEnemy();

        }
    }

    private void SpawnTankEnemy()
    {
        int enemyIndex = Random.Range(0, EnemyPrefabs.Length);
        GameObject enemyToSpawn = EnemyPrefabs[enemyIndex];

        float offsetX = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        float offsetY = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0);

        GameObject tankenemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        // getcomponent off of whatever enemy to spawn is
        // if it has the TankEnemyScript, do that
        // else if it has the StalkerEnemy, do that
        // else Normal do that
        
        TankEnemyScript tankenemyScript = tankenemy.GetComponent<TankEnemyScript>();
        if (tankenemyScript != null)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            tankenemyScript.SetTarget(playerTransform);
        }
    }
    
    private void SpawnStalkerEnemy()
    {
        int enemyIndex = Random.Range(0, EnemyPrefabs.Length);
        GameObject enemyToSpawn = EnemyPrefabs[enemyIndex];

        float offsetX = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        float offsetY = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0);

        GameObject stalkerenemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        StalkerEnemyScript stalkerenemyScript = stalkerenemy.GetComponent<StalkerEnemyScript>();
        if (stalkerenemyScript != null)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            stalkerenemyScript.SetTarget(playerTransform);
        }
    }
    
    private void SpawnNormalEnemy()
    {
        int enemyIndex = Random.Range(0, EnemyPrefabs.Length);
        GameObject enemyToSpawn = EnemyPrefabs[enemyIndex];

        float offsetX = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        float offsetY = Random.Range(-SpawnAreaSize, SpawnAreaSize);
        Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0);

        GameObject normalenemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        NormalEnemyScript normalenemyScript = normalenemy.GetComponent<NormalEnemyScript>();
        if (normalenemyScript != null)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            normalenemyScript.SetTarget(playerTransform);
        }
    }

    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, RotationSpeed * Time.deltaTime);
    }
}