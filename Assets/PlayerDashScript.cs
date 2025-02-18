using UnityEngine;

public class PlayerDashScript : MonoBehaviour
{
    public SpriteRenderer DashBar;
    public PlayerScript Player;
    public Vector3 DashBarOffset = new Vector3(0, -0.4f, -1f);

    private void Start()
    {
        DashBar.transform.SetParent(null);
    }

    private void FixedUpdate()
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

        DashBar.transform.position = Player.transform.position + DashBarOffset;

        DashBar.transform.localScale = new Vector3((Player.Dash / Player.MaxDash) * 6, .65f, .1f);

        DashBar.transform.rotation = Quaternion.identity;
    }
}