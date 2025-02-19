using UnityEngine;

public class StalkerEnemyScript : MonoBehaviour
{
    public float speed = 1f;       
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
    
    public AudioClip stalkerSfx;       
    public AudioSource audioSource;      
    private bool hasPlayedSound = false; 

    public float rotationSpeed = 0.1f;
    public float movementSmoothTime = 0.3f;

    private Vector3 currentVelocity;
    
    public void Start()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerScript>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 2.5f;
    }

    public void Update()
    {
        target = player.transform;

        Vector3 targetPosition = target.position;
        transform.position = Vector3.SmoothDamp(transform.position, 
            targetPosition, ref currentVelocity, movementSmoothTime, speed);

        if (StalkerHealth <= 0)
        {
            Destroy(gameObject);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 30f && !hasPlayedSound)
        {
            audioSource.PlayOneShot(stalkerSfx);
            hasPlayedSound = true;
        }

        targetaim = new Vector2(target.transform.position.x, target.transform.position.y);
        targetPos = targetaim;
        thisPos = transform.position;

        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 180f;

        turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation,
            Quaternion.Euler(0, 0, angle + 90), rotationSpeed);

        targetaim = Vector2.up;
        targetPos = targetaim;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 180f;

        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.Euler(0, 0, angle + 90), rotationSpeed);
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