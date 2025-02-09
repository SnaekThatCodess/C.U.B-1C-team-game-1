using UnityEngine;
using TMPro;
using System.Collections.Generic;
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
    public List<BulletScript> bullets;

    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerUI();
        timerText.color = Color.white;
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        UpdateTimerUI();

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(Player.transform.position.x, Player.transform.position.y, -10), .05f);
    }

    void UpdateGameSettings(float bulletSpeed, float shootCooldown, float minSpawnRate,
        float maxSpawnRate, float rotationSpeed, float normalenemyspeed, float tankenemyspeed, float stalkerenemyspeed)
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
            if (NormalEnemies != null)
            {
                normalenemy.speed = normalenemyspeed;
            }
        }
        
        foreach (var tankenemy in TankEnemies)
        {
            if (TankEnemies != null)
            {
                tankenemy.speed = tankenemyspeed;
            }
        }
        
        foreach (var stalkerenemy in StalkerEnemies)
        {
            if (NormalEnemies != null)
            {
                stalkerenemy.speed = stalkerenemyspeed;
            }
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = $"{Mathf.Max(0, Mathf.FloorToInt(currentTime))}";
    }
}