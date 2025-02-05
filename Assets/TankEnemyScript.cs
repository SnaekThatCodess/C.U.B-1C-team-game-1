using System.Collections.Generic;
using UnityEngine;

public class TankEnemyScript : MonoBehaviour
{
    public float speed = 1.5f;
    private Transform target;
    public float TankHealth = 10;

    //public List<AudioClip> ded;

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
        if (TankHealth <= 1)
        {
            Destroy(gameObject);
            player.Score += 2;
            player.UpdateScore();
        }
        else
        {
            TankHealth--;
        }
    }
}