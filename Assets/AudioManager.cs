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
        audioSource.time = currentTime;
        audioSource.Play();
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
        if (audioSource.isPlaying)
        {
            audioSource.time = currentTime;
            audioSource.Play();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}