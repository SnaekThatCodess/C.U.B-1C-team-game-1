using UnityEngine;

public class BulletTankScript : MonoBehaviour
{
    public float Speed = 10f;
    public float Lifetime = 2f;
    private Vector3 direction;
    private PlayerScript player;
    private TankEnemyScript tankenemy;

    public Rigidbody2D RB;

    private void Start()
    {
        Destroy(gameObject, Lifetime);
        
        player = FindObjectOfType<PlayerScript>();

        Vector3 offset = player.transform.position - transform.position;
        SetDirection(offset);

        if (RB != null)
        {
            RB.velocity = direction * Speed;
        }
    }

    private void Update()
    {
        if (RB == null)
        {
            transform.Translate(direction * (Speed * Time.deltaTime), Space.World);
        }
    }

    public void Initialize(TankEnemyScript tankenemy)
    {
        this.tankenemy = tankenemy;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            player.Health -= 4;
            Destroy(gameObject);
        }
    }
}