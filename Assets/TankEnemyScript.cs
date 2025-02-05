using System.Collections.Generic;
using UnityEngine;

public class TankEnemyScript : MonoBehaviour
{
    public float speed = 1.5f;
    private Transform target;
    public float TankHealth = 10;
    public GameObject player;

    public PlayerScript pc;

    public void Start()
    {
        player=GameObject.Find("player");
        pc = player.GetComponent<PlayerScript>();
    }

    public void Update()
    {
        target = player.transform;
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);
        if (TankHealth <= 1)
        {
            Destroy(gameObject);
        }
    }

    public void GetBumped()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        pc.Score += 1;
        pc.UpdateScore();
    }
}