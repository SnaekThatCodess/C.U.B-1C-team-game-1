using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUp : MonoBehaviour
{
    
    public TextMeshPro TitleText;
    public float Timer = .45f;
    
    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            TitleText.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            Timer = 2.4f;
        }
        
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Tutorial1");
        }
    }
}
