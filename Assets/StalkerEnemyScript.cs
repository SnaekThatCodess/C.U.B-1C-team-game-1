using UnityEngine;

public class StalkerEnemyScript : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    public float StalkerHealth = 3;
    public GameObject player;
    public GameObject turret;
    public Animator anim;
    public Vector2 targetaim;
    public Vector2 targetPos;
    public Vector2 thisPos;
    public float angle;

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
        if (StalkerHealth <= 1)
        {
            Destroy(gameObject);
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
        pc.Score += 3;
        pc.UpdateScore();
    }
}