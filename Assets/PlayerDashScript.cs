using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashScript : MonoBehaviour
{
    public SpriteRenderer DashBar;
    public PlayerScript Player;
    //public float Stamina = 100;
    //public float MaxStamina = 100;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Player.RS == PlayerScript.RunState.DashActive)
        {
            DashBar.color = Color.yellow;
        }
        
        if (Player.RS == PlayerScript.RunState.DashRecover)
        {
            DashBar.color = Color.red;
        }
        
        if (Player.RS == PlayerScript.RunState.DashReady)
        {
            DashBar.color = Color.green;

            if (Player.Dash <= 100)
            {
                DashBar.color = Color.clear;
            }
        }
        
        DashBar.transform.localScale = new Vector3((Player.Dash / Player.MaxDash) * 1.82f, .325f, 1);
    }
}