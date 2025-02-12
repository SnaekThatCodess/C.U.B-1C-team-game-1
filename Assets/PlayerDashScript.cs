using UnityEngine;

public class PlayerDashScript : MonoBehaviour
{
    public SpriteRenderer DashBar;
    public PlayerScript Player;
    

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
        }
        
        DashBar.transform.localScale = new Vector3((Player.Dash / Player.MaxDash) * 1, .1f, .1f);
    }
}