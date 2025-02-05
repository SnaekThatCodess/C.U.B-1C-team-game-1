using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
