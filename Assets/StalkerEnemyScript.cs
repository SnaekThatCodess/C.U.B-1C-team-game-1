using System.Collections.Generic;
using UnityEngine;

public class StalkerEnemyScript : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    public float StalkerHealth = 3;

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
        if (StalkerHealth <= 1)
        {
            Destroy(gameObject);
            player.Score += 3;
            player.UpdateScore();
        }
        else
        {
            StalkerHealth--;
        }
    }
}