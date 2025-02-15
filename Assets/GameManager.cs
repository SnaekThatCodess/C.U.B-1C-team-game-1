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
    public TextMeshPro EnemiesLeft;

    public int totalEnemiesKilled = 0;
    public int enemiesInCurrentWave = 10;

    public Camera mainCamera;
    public Transform PlayerTransform;

    void Start()
    {
        //OnEnemyKilled();
        currentTime = countdownTime;
        UpdateTimerUI();
        timerText.color = Color.white;
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        UpdateTimerUI();

        if (mainCamera != null && Player != null)
        {
            Vector3 targetPosition = new Vector3
                (Player.transform.position.x, Player.transform.position.y, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.Lerp
                (mainCamera.transform.position, targetPosition, 0.075f);
        }
    }

    void UpdateGameSettings(float playerbulletSpeed, float normalbulletSpeed, float tankbulletSpeed,
        float shootCooldown, float minSpawnRate, float maxSpawnRate, float rotationSpeed, 
        float normalenemyspeed, float tankenemyspeed, float stalkerenemyspeed)
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
}