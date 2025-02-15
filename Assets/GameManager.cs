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

    private int currentWave = 1;
    private int totalEnemiesKilled = 0;
    private int enemiesInCurrentWave = 10;

    public Camera mainCamera;
    public Transform PlayerTransform;

    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerUI();
        timerText.color = Color.white;
        UpdateWaveUI();
        
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
                (mainCamera.transform.position, targetPosition, 0.08f);
        }

        // Removed the call to OnEnemyKilled() from FixedUpdate() (as mentioned earlier)
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

    public void OnEnemyKilled()
    {
        totalEnemiesKilled++;

        if (totalEnemiesKilled >= enemiesInCurrentWave)
        {
            StartWave();
        }
    }

    void StartWave()
    {
        if (totalEnemiesKilled < enemiesInCurrentWave)
            return;

        currentWave++;
        totalEnemiesKilled = 0;
        enemiesInCurrentWave = 10 + currentWave * 2;

        DespawnAllEnemies();
        
        DespawnAllBullets();

        float newPlayerBulletSpeed = 5f * currentWave;
        float newNormalBulletSpeed = 6f * currentWave;
        float newTankBulletSpeed = 4.5f * currentWave;
        float newShootCooldown = 1f / currentWave;
        float newMinSpawnRate = 1f / currentWave;
        float newMaxSpawnRate = 1f / (currentWave * 0.5f);
        float newRotationSpeed = 10f * currentWave;
        float newNormalEnemySpeed = 2f * currentWave;
        float newTankEnemySpeed = 1.5f * currentWave;
        float newStalkerEnemySpeed = 2.5f * currentWave;

        UpdateGameSettings(newPlayerBulletSpeed, newNormalBulletSpeed, 
            newTankBulletSpeed, newShootCooldown, newMinSpawnRate, newMaxSpawnRate, newRotationSpeed,
            newNormalEnemySpeed, newTankEnemySpeed, newStalkerEnemySpeed);

        Player.Health += 1;

        Player.Speed *= 0.99f;
        Player.shootCooldown *= 1.01f;
        Player.Dash *= 0.99f;

        UpdateWaveUI();

        Debug.Log("Wave " + currentWave + " started!");
    }

    void UpdateWaveUI()
    {
        WaveText.text = "Wave: " + currentWave;
        EnemiesLeft.text = "Enemies Left: " + (enemiesInCurrentWave - totalEnemiesKilled);
    }

    void DespawnAllEnemies()
    {
        foreach (var normalenemy in NormalEnemies)
        {
            Destroy(normalenemy.gameObject);
        }

        foreach (var tankenemy in TankEnemies)
        {
            Destroy(tankenemy.gameObject);
        }

        foreach (var stalkerenemy in StalkerEnemies)
        {
            Destroy(stalkerenemy.gameObject);
        }
    }
    
    void DespawnAllBullets()
    {
        foreach (var normalbullet in normalBullets)
        {
            Destroy(normalbullet.gameObject);
        }

        foreach (var tankbullet in tankBullets)
        {
            Destroy(tankbullet.gameObject);
        }

        foreach (var playerbullet in playerBullets)
        {
            Destroy(playerbullet.gameObject);
        }
    }
}