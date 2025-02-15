using UnityEngine;
using Random = UnityEngine.Random;

public class NormalEnemyScript : MonoBehaviour
{
    public float speed = 4f;
    private Transform target;
    public float NormalHealth = 5;
    public GameObject player;
    public GameObject turret;
    public Animator anim;
    public Vector2 targetaim;
    public Vector2 targetPos;
    public Vector2 thisPos;
    public float angle;

    public PlayerScript pc;

    public GameObject NormalBulletPrefab;
    public Transform normalCenter;

    private float nextShootTime;
    private float randomShootInterval;

    public GameManager GM;

    public void Start()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerScript>();

        randomShootInterval = Random.Range(2f, 7f);
        nextShootTime = Time.time + randomShootInterval;
        anim.Play("normaldrive");
    }

    public void Update()
    {
        target = player.transform;
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);

        if (NormalHealth <= 1)
        {
            Destroy(gameObject);
        }

        if (Time.time >= nextShootTime)
        {
            Shoot();
            randomShootInterval = Random.Range(2f, 5f);
            nextShootTime = Time.time + randomShootInterval;
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
        transform.rotation = Quaternion.Euler(0, 0, angle+90);
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
        
        anim.Play("normalshoot");
        if (NormalBulletPrefab != null)
        {
            Quaternion bulletRotation = normalCenter.rotation;

            Vector3 spawnPosition = transform.position + bulletRotation * new Vector3(0, 2f, 0);

            GameObject bullet = Instantiate(NormalBulletPrefab, spawnPosition, bulletRotation);

            BulletNormalScript bulletNormalScript = bullet.GetComponent<BulletNormalScript>();
            bulletNormalScript.Initialize(this);

            Vector3 direction = bullet.transform.up;
            bulletNormalScript.SetDirection(direction);
        }
    }
}