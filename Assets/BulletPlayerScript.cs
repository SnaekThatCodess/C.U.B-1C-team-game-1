using UnityEngine;

public class BulletPlayerScript : MonoBehaviour
{
    public float Speed = 10f;
    public float Lifetime = 2f;
    private Vector3 direction;
    private PlayerScript player;
    private NormalEnemyScript normalenemy;
    private TankEnemyScript tankenemy;
    private StalkerEnemyScript stalkerenemy;

    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * (Speed * Time.deltaTime), Space.World);
    }

    public void Initialize(PlayerScript player)
    {
        this.player = player;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NormalEnemyScript normalenemy = other.GetComponent < NormalEnemyScript>();
        if (normalenemy != null)
        {
            normalenemy.NormalHealth--;
            Destroy(gameObject);
        }
        
        TankEnemyScript tankenemy = other.GetComponent < TankEnemyScript>();
        if (tankenemy != null)
        {
            tankenemy.TankHealth--;
            Destroy(gameObject);
        }
        
        StalkerEnemyScript stalkerenemy = other.GetComponent < StalkerEnemyScript>();
        if (stalkerenemy != null)
        {
            stalkerenemy.StalkerHealth--;
            Destroy(gameObject);
        }
    }
}
