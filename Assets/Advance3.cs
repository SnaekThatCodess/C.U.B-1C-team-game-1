using UnityEngine;
using UnityEngine.SceneManagement;

public class Advance3 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Game");
        }
    }
}