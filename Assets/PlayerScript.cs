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
    }

    private void HandleMovement()
    {
        Vector2 vel = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.D)) vel.x = Speed;
        if (Input.GetKey(KeyCode.A)) vel.x = -Speed;
        if (Input.GetKey(KeyCode.W)) vel.y = Speed;
        if (Input.GetKey(KeyCode.S)) vel.y = -Speed;

        RB.velocity = vel;
    }

    private void UpdateRotation()
    {
        playerCenter.Rotate(0,0,0);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerCenter.Rotate(0, 0, 300 * Time.deltaTime);
        }
      
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerCenter.Rotate(0,0,-300 * Time.deltaTime);
        }

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
            normalenemy.GetBumped(this);
            Die();
        }
        
        TankEnemyScript tankenemy = other.gameObject.GetComponent<TankEnemyScript>();
        if (tankenemy != null)
        {
            tankenemy.GetBumped(this);
            Die();
        }
        
        StalkerEnemyScript stalkerenemy = other.gameObject.GetComponent<StalkerEnemyScript>();
        if (stalkerenemy != null)
        {
            stalkerenemy.GetBumped(this);
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
        if (Health <= 1)
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