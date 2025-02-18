using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

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

        foreach (var spawner in spawners)
        {
            spawner.gameManager = this;
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
    
    public void AddBullet(BulletPlayerScript bullet)
    {
        playerBullets.Add(bullet);
    }

    public void RemoveBullet(BulletPlayerScript bullet)
    {
        playerBullets.Remove(bullet);
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

    void DestroyAllEnemiesAndBullets()
    {
        foreach (var enemy in NormalEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        NormalEnemies.Clear();

        foreach (var tankEnemy in TankEnemies)
        {
            if (tankEnemy != null)
            {
                Destroy(tankEnemy.gameObject);
            }
        }
        TankEnemies.Clear();

        foreach (var stalkerEnemy in StalkerEnemies)
        {
            if (stalkerEnemy != null)
            {
                Destroy(stalkerEnemy.gameObject);
            }
        }
        StalkerEnemies.Clear();

        foreach (var playerBullet in playerBullets)
        {
            if (playerBullet != null)
            {
                Destroy(playerBullet.gameObject);
            }
        }
        playerBullets.Clear();

        foreach (var normalBullet in normalBullets)
        {
            if (normalBullet != null)
            {
                Destroy(normalBullet.gameObject);
            }
        }
        normalBullets.Clear();

        foreach (var tankBullet in tankBullets)
        {
            if (tankBullet != null)
            {
                Destroy(tankBullet.gameObject);
            }
        }
        tankBullets.Clear();
    }

    void UpdateWave()
    {
        if (Player.Health >= 1 && timeUntilNextWave <= 0)
        {
            DestroyAllEnemiesAndBullets();

            currentWave++;
            Player.Health++;

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
}