using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Color = UnityEngine.Color;

public class NormalEnemyScript : MonoBehaviour
{
    public float speed = 4f;
    private Transform target;
    public float NormalHealth = 5;
    public PlayerScript player;

    public void SetTarget(Transform playerTransform)
    {
        target = playerTransform;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * (speed * Time.deltaTime);
        }
    }

    public void GetBumped(PlayerScript player)
    {
        if (NormalHealth <= 1)
        {
            Destroy(gameObject);
            player.Score += 1;
            player.UpdateScore();
        }
        else
        {
            NormalHealth--;
        }
    }
}