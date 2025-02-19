using UnityEngine;
using UnityEngine.SceneManagement;

public class Advance2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Tutorial3");
        }
    }
}