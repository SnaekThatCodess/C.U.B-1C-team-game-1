using UnityEngine;
using UnityEngine.SceneManagement;

public class Advance1 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Tutorial2");
        }
    }
}
