using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager4 : MonoBehaviour
{
    public static GameManager4 Instance { get; private set; }

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
    public float currentWave = 2;
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
        currentWave = 2;
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
            spawner.gameManager4 = this;
        }

        UpdateGameSettings
        (
            6.4f,
            5.6f,
            4.15f,
            0.775f,
            7.25f,
            14.75f,
            55f,
            5.6f,
            4.15f,
            9.15f
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
        
        UpdateWaveUI();
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

    void UpdateWaveUI()
    {
        WaveText.text = "Wave: " + currentWave;
    }

    void UpdateWave()
    {
        if (Player.Health >= 1 && timeUntilNextWave <= 0)
        {
            SceneManager.LoadScene("Game3");
        }
    }
}