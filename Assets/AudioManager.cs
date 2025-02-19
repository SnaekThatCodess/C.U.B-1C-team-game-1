using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    private float currentTime = 0f;

    public AudioClip audioClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HandleAudioForScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            currentTime = audioSource.time;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleAudioForScene(scene.name);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void HandleAudioForScene(string sceneName)
    {
        if (sceneName == "Start" || sceneName == "Tutorial1" || sceneName == "Tutorial2" || sceneName == "Tutorial3")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.loop = true;
                audioSource.time = currentTime;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
}