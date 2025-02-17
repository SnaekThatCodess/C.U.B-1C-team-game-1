using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

public class GameManager : MonoBehaviour
{
    public TextMeshPro timerText;
    public float countdownTime = 0f;
    private float currentTime;

    public PlayerScript Player;
    public List<EnemySpawner> spawners;
    public List<NormalEnemyScript> NormalEnemies;
    public List<TankEnemyScript> TankEnemies;
    public List<StalkerEnemyScript> StalkerEnemies;
    public List<BulletPlayerScript> playerBullets;
    public List<BulletNormalScript> normalBullets;
    public List<BulletTankScript> tankBullets;

    public TextMeshPro WaveText;
    public TextMeshPro WaveTimeLeft;
    private float timeUntilNextWave;
    public float currentWave = 1;
    public float waveTimer = 30;

    public Camera mainCamera;
    public Transform PlayerTransform;

    public GameObject normalEnemyPrefab;
    public GameObject tankEnemyPrefab;
    public GameObject stalkerEnemyPrefab;

    void Start()
    {
        currentWave = 1;
        currentTime = countdownTime;
        timeUntilNextWave = waveTimer;
        UpdateTimerUI();
        timerText.color = Color.white;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        UpdateGameSettings
        (
            6.5f,
            5.5f,
            4f,
            0.75f,
            7.5f,
            15f,
            50f,
            5.5f,
            4f,
            9f
            
            // * difficultyMultiplier
        );
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        UpdateTimerUI();

        timeUntilNextWave -= Time.deltaTime;
        UpdateWaveTimerUI();

        if (mainCamera != null && Player != null)
        {
            Vector3 targetPosition = new Vector3
                (Player.transform.position.x, Player.transform.position.y, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.Lerp
                (mainCamera.transform.position, targetPosition, 0.075f);
        }

        UpdateWave();
    }

    void UpdateGameSettings(
        float playerbulletSpeed,
        float normalbulletSpeed,
        float tankbulletSpeed,
        float shootCooldown,
        float minSpawnRate,
        float maxSpawnRate,
        float rotationSpeed,
        float normalenemyspeed,
        float tankenemyspeed,
        float stalkerenemyspeed)
    {
        if (Player != null)
        {
            Player.shootCooldown = shootCooldown;
        }

        foreach (var spawner in spawners)
        {
            if (spawner != null)
            {
                spawner.MinSpawnRate = minSpawnRate;
                spawner.MaxSpawnRate = maxSpawnRate;
                spawner.RotationSpeed = rotationSpeed;
            }
        }

        foreach (var normalenemy in NormalEnemies)
        {
            if (normalenemy != null)
            {
                normalenemy.speed = normalenemyspeed;
            }
        }

        foreach (var tankenemy in TankEnemies)
        {
            if (tankenemy != null)
            {
                tankenemy.speed = tankenemyspeed;
            }
        }

        foreach (var stalkerenemy in StalkerEnemies)
        {
            if (stalkerenemy != null)
            {
                stalkerenemy.speed = stalkerenemyspeed;
            }
        }

        foreach (var playerbullet in playerBullets)
        {
            if (playerbullet != null)
            {
                playerbullet.Speed = playerbulletSpeed;
            }
        }

        foreach (var normalbullet in normalBullets)
        {
            if (normalbullet != null)
            {
                normalbullet.Speed = normalbulletSpeed;
            }
        }

        foreach (var tankbullet in tankBullets)
        {
            if (tankbullet != null)
            {
                tankbullet.Speed = tankbulletSpeed;
            }
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = $"{Mathf.Max(0, Mathf.FloorToInt(currentTime))}";
    }

    void UpdateWaveTimerUI()
    {
        WaveTimeLeft.text = $"Time Left: {Mathf.Max(0, Mathf.FloorToInt(timeUntilNextWave))}";
    }

    IEnumerator ReverseAndMagnifySpeedForSeconds(GameObject obj, float duration, float speedMultiplier)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 originalVelocity = rb.velocity;

            rb.velocity = -originalVelocity * speedMultiplier;

            yield return new WaitForSeconds(duration);

            rb.velocity = originalVelocity;
        }
        else
        {
            Vector3 originalPosition = obj.transform.position;
            Vector3 directionToPlayer = (obj.transform.position - Player.transform.position).normalized;

            obj.transform.position += directionToPlayer * speedMultiplier;

            yield return new WaitForSeconds(duration);

            obj.transform.position = originalPosition;
        }
    }

    void SendObjectsFarAwayFromPlayer()
    {
        foreach (var enemy in NormalEnemies)
        {
            if (enemy != null)
            {
                StartCoroutine(ReverseAndMagnifySpeedForSeconds(enemy.gameObject, 15f, 10f));
            }
        }

        foreach (var tankEnemy in TankEnemies)
        {
            if (tankEnemy != null)
            {
                StartCoroutine(ReverseAndMagnifySpeedForSeconds(tankEnemy.gameObject, 15f, 10f));
            }
        }

        foreach (var stalkerEnemy in StalkerEnemies)
        {
            if (stalkerEnemy != null)
            {
                StartCoroutine(ReverseAndMagnifySpeedForSeconds(stalkerEnemy.gameObject, 15f, 10f));
            }
        }

        foreach (var playerBullet in playerBullets)
        {
            if (playerBullet != null)
            {
                StartCoroutine(ReverseAndMagnifySpeedForSeconds(playerBullet.gameObject, 15f, 10f));
            }
        }

        foreach (var normalBullet in normalBullets)
        {
            if (normalBullet != null)
            {
                StartCoroutine(ReverseAndMagnifySpeedForSeconds(normalBullet.gameObject, 15f, 10f));
            }
        }

        foreach (var tankBullet in tankBullets)
        {
            if (tankBullet != null)
            {
                StartCoroutine(ReverseAndMagnifySpeedForSeconds(tankBullet.gameObject, 15f, 10f));
            }
        }
    }

    void UpdateWave()
    {
        if (Player.Health >= 1 && timeUntilNextWave <= 0)
        {
            currentWave++;
            Player.Health++;

            SendObjectsFarAwayFromPlayer();
        
            WaveText.text = "Wave " + currentWave;

            if (currentWave >= 5)
            {
                Player.Health++;
            }
            if (currentWave >= 15)
            {
                Player.Health += 2;
            }
            if (currentWave >= 30)
            {
                Player.Health += 3;
            }
            if (currentWave >= 50)
            {
                Player.Health += 4;
            }

            float difficultyMultiplier = 1 + (currentWave * 0.02f);

            UpdateGameSettings
            (
                6.5f * difficultyMultiplier,
                5.5f * difficultyMultiplier,
                4f * difficultyMultiplier,
                0.75f * difficultyMultiplier,
                7.5f * difficultyMultiplier,
                15f * difficultyMultiplier,
                50f * difficultyMultiplier,
                5.5f * difficultyMultiplier,
                4f * difficultyMultiplier,
                9f * difficultyMultiplier
            );

            timeUntilNextWave = 30f * Mathf.Pow(1.02f, currentWave);

            WaveText.text = "Wave " + currentWave;
        }
    }

    void SpawnEnemiesForWave()
    {
        int numberOfNormalEnemies = Mathf.FloorToInt(currentWave * 1.5f);
        int numberOfTankEnemies = Mathf.FloorToInt(currentWave * 0.75f);
        int numberOfStalkerEnemies = Mathf.FloorToInt(currentWave * 0.5f); 
        
        int spawnerCount = spawners.Count;

        if (spawnerCount == 0) return;

        for (int i = 0; i < spawnerCount; i++)
        {
            var spawner = spawners[i];

            int spawnerNormalEnemies = Mathf.FloorToInt(numberOfNormalEnemies / (float)spawnerCount);
            int spawnerTankEnemies = Mathf.FloorToInt(numberOfTankEnemies / (float)spawnerCount);
            int spawnerStalkerEnemies = Mathf.FloorToInt(numberOfStalkerEnemies / (float)spawnerCount);

            spawner.EnemyPrefabs = new GameObject[] { normalEnemyPrefab, tankEnemyPrefab, stalkerEnemyPrefab };
            spawner.MinSpawnRate =
                Mathf.Clamp(spawner.MinSpawnRate * (1 + currentWave * 0.02f), 1f,
                    10f);
            spawner.MaxSpawnRate =
                Mathf.Clamp(spawner.MaxSpawnRate * (1 + currentWave * 0.02f), 1f,
                    10f);
        }
    }
}