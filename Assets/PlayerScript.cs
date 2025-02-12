using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public AudioSource Audio;
    public AudioClip PlayerDeath;
    public AudioClip fire;
    public AudioClip ouch;
    public GameObject BulletPrefab;
    public Transform FirePoint;

    public TextMeshPro ScoreText;

    public TextMeshPro HealthInicator;
    public SpriteRenderer HealthSprite;
    public TextMeshPro HealthText;
    
    public TextMeshPro HighscoreText;

    public TextMeshPro WaveText;

    public float Speed = 5;
    public int Score = 0;
    public int Health = 10;
    public int Wave = 1;
    public static int Highscore = 0;

    public SpriteRenderer SR;

    public float shootCooldown = 0.5f;
    public float nextShootTime = 0f;
    
    public Transform playerCenter;
    public GameObject turret;
    public Animator anim;
    public Vector2 targetaim;
    public Vector2 targetPos;
    public Vector2 thisPos;
    public float angle;
    
    public float Dash;
    public float MaxDash = 100;
    public float DashTimer;
    public float DashRecoverTimer;
    public PlayerDashScript PlayerDash;
    public float SpeedMultiplier = 1;

    private void Start()
    {
        UpdateScore();
        HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0);
    }

    void Update()
    {
        UpdateHealth();
        HandleMovement();
        HandleShooting();
        UpdateHighscore();
        UpdateRotation();
        
        targetaim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = targetaim;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.LerpAngle(angle,Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 180f,0.03f);
        turret.transform.rotation = Quaternion.Euler(0, 0, angle+90);
        
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (RS != RunState.DashRecover)
            {
                RS = RunState.DashActive;
            }
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            if (RS != RunState.DashRecover)
            {
                RS = RunState.DashReady;
            }
    
            if (Dash == 0)
            {
                RS = RunState.DashReady;
                DashTimer = 10;
            }
        }

        if (SpeedMultiplier > 1)
        {
            SpeedMultiplier = 1;
        }
        
        if (SpeedMultiplier < 0)
        {
            SpeedMultiplier = 0;
        }

        GetSpeed();
        UpdateDash();
    }
    
    public RunState RS;

    public void GetSpeed()
    {
        if (RS == RunState.DashReady)
        {
            Debug.Log("dashready");
            SpeedMultiplier = 1;
        }
        else if (RS == RunState.DashActive)
        {
            Debug.Log("dashactive");
            SpeedMultiplier=1.8f;
        }
        else if (RS == RunState.DashRecover)
        {
            Debug.Log("dashrecover");
            SpeedMultiplier=0.4f;
        }
    }
    
    public enum RunState
    {
        DashReady,
        DashActive,
        DashRecover,
    }

    public void UpdateDash()
    {
        if (RS == RunState.DashActive)
        {
            Dash -= Time.deltaTime * (100/.75f);
        }
        
        if (RS == RunState.DashReady)
        {
            Dash += Time.deltaTime * (100/5f);
        }

        if (RS == RunState.DashRecover)
        {
            DashRecoverTimer -= Time.deltaTime;
            Dash += Time.deltaTime * (100/10f);
            if (DashRecoverTimer <= 0)
            {
                RS = RunState.DashReady;
                Dash = 100;
            }
        }

        if (Dash > 100)
        {
            Dash = 100;
        }
        if (Dash < 0)
        {
            Dash = 0;
            RS = RunState.DashRecover;
            DashRecoverTimer = 10;
        }
    }

    private void HandleMovement()
    {
        Vector2 vel = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.D)) vel.x = Speed*SpeedMultiplier;
        if (Input.GetKey(KeyCode.A)) vel.x = -Speed*SpeedMultiplier;
        if (Input.GetKey(KeyCode.W)) vel.y = Speed*SpeedMultiplier;
        if (Input.GetKey(KeyCode.S)) vel.y = -Speed*SpeedMultiplier;

        RB.velocity = vel;
        anim.Play("playerdrive");
    }

    private void UpdateRotation()
    {
        //**lets change it so that the player turns in the way its moving**
        
        // playerCenter.Rotate(0,0,0);
        //
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     playerCenter.Rotate(0, 0, 300 * Time.deltaTime);
        // }
        //
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     playerCenter.Rotate(0,0,-300 * Time.deltaTime);
        // }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    public void Shoot()
    {
        anim.Play("playershoot");
        if (BulletPrefab != null)
        {
            Quaternion bulletRotation = playerCenter.rotation;

            Vector3 spawnPosition = transform.position + bulletRotation * new Vector3(0, 2f, 0);
        
            GameObject bullet = Instantiate(BulletPrefab, spawnPosition, bulletRotation);

            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.Initialize(this);

            Vector3 direction = bullet.transform.up; 
            bulletScript.SetDirection(direction);
        
            Audio.PlayOneShot(fire);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NormalEnemyScript normalenemy = other.gameObject.GetComponent<NormalEnemyScript>();
        if (normalenemy != null)
        {
            normalenemy.GetBumped();
            Die();
        }
        
        TankEnemyScript tankenemy = other.gameObject.GetComponent<TankEnemyScript>();
        if (tankenemy != null)
        {
            tankenemy.GetBumped();
            Die();
        }
        
        StalkerEnemyScript stalkerenemy = other.gameObject.GetComponent<StalkerEnemyScript>();
        if (stalkerenemy != null)
        {
            stalkerenemy.GetBumped();
            Health--;
            Die();
        }
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
    }

    public void UpdateHealth()
    {
        HealthText.text = "" + Health;
    }

    public void UpdateHighscore()
    {
        if (Score > PlayerPrefs.GetInt("Personal Best", 0))
        {
            PlayerPrefs.SetInt("Personal Best", Score);
        }
        HighscoreText.text = "Personal Best: " + PlayerPrefs.GetInt("Personal Best", 0);
    }

    public void Die()
    {
        if (Health < 1)
        {
            Speed = 0;
            SR.color = Color.white;
            Audio.PlayOneShot(PlayerDeath);
            Invoke("LoadLoseScene", 1f);
        }
        else
        {
            Health--;
            Audio.PlayOneShot(ouch);
        }
    }

    private void LoadLoseScene()
    {
        SceneManager.LoadScene("Lose");
    }
}