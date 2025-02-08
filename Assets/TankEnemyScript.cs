using UnityEngine;
using Random = UnityEngine.Random;

public class TankEnemyScript : MonoBehaviour
{
    public float speed = 1.5f;
    private Transform target;
    public float TankHealth = 10;
    public GameObject player;
    public GameObject turret;
    public Animator anim;
    public Vector2 targetaim;
    public Vector2 targetPos;
    public Vector2 thisPos;
    public float angle;

    public PlayerScript pc;

    public GameObject[] TankBulletPrefabs;

    public float shootCooldown = 3f;
    public float nextShootTime = 1f;
    
    public Transform tankCenter;
    
    public float normalBulletTimer = 5f;
    private float currentTime;
    
    private float randomShootInterval;

    [Header("Bullet Spread Angles")]
    public float[] spreadAngles = {-25f, 25f, -57.5f, 57.5f};

    void Start()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerScript>();
        
        randomShootInterval = Random.Range(2f, 5f);
        nextShootTime = Time.time + randomShootInterval;
    }

    void Update()
    {
        
        normalBulletTimer -= Time.deltaTime;
        
        target = player.transform;
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);

        if (TankHealth <= 1)
        {
            Destroy(gameObject);
        }

        if (normalBulletTimer <= 1)
        {
            HandleShooting();
        }

        targetaim = new Vector2(target.transform.position.x,target.transform.position.y);
        targetPos = targetaim;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 180f;
        turret.transform.rotation = Quaternion.Euler(0, 0, angle+90);
        targetaim = Vector2.up;
        targetPos = targetaim;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 180f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void HandleShooting()
    {
        if (Time.time >= nextShootTime)
        {
            anim.Play("tankshoot");
            Shoot();
            nextShootTime = Time.time + shootCooldown;
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

    public void Shoot()
    {
        if (TankBulletPrefabs.Length > 0)
        {
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;

            foreach (float angle in spreadAngles)
            {
                Quaternion spreadRotation = Quaternion.Euler(0, 0, angle); 
                Vector3 spreadDirection = spreadRotation * playerDirection;

                GameObject bullet = new GameObject("TankBullet");
                BulletTankScript bulletScript = bullet.AddComponent<BulletTankScript>();

                bulletScript.Initialize(this);

                bulletScript.SetDirection(spreadDirection);

                Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
                rb.velocity = spreadDirection * bulletScript.Speed;

                bullet.transform.position = transform.position + tankCenter.up * 2f;

                Destroy(bullet, bulletScript.Lifetime);
            }
        }
    }
}