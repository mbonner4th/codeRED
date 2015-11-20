using UnityEngine;
using System.Collections;

public class BajaBlastEffect : MonoBehaviour
{

    private bool startTimer = true;
    private float timerDuration = 6.0f;
    private UnityStandardAssets._2D.PlatformerCharacter2D myPlayer;

    // Use this for initialization
    void Start () {
	
	}

    public void setMyPlayer(UnityStandardAssets._2D.PlatformerCharacter2D myPlayer)
    {
        this.myPlayer = myPlayer;
    }

    // Update is called once per frame
    void Update ()
    {
	    if(startTimer)
        {
            if(timerDuration <= 0)
            {
                startTimer = false;
                slowPlayers();
                Destroy(this.gameObject);
            }
            else
            {
                timerDuration -= Time.deltaTime;
            }
        }
	}

    void slowPlayers()
    {
        UnityStandardAssets._2D.PlatformerCharacter2D[] players = FindObjectsOfType<UnityStandardAssets._2D.PlatformerCharacter2D>();
        foreach(UnityStandardAssets._2D.PlatformerCharacter2D p in players)
        {
            if(p.Equals(myPlayer))
            {
                p.multiplyJump(0.8f);
                p.multiplySpeed(0.8f);
            }
        }
    }
}
