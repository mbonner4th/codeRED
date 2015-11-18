using UnityEngine;
using System.Collections;

public class StopWatch : Weapon {

    private bool timerStarted = false;
    private float timerDuration = 6.0f;
	// Use this for initialization
	void Start ()
    {
        timerDuration = 6.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timerStarted)
        {
            if (timerDuration <= 0)
            {
                stopTimer();
                timerStarted = false;
            }
            else
            {
                timerDuration -= Time.deltaTime;
            }
        }   
	}

    void startTimer()
    {
        Object[] o = FindObjectsOfType(typeof(Player));
        foreach(Player p in o)
        {
            if(p.playerNum != this.transform.parent.parent.GetComponent<Player>().playerNum)
            {
                p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplySpeed(0.5f);
                p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplyJump(0.5f);
            }
        }
    }

    void stopTimer()
    {
        Object[] o = FindObjectsOfType(typeof(Player));
        foreach (Player p in o)
        {
            if (p.playerNum != this.transform.parent.parent.GetComponent<Player>().playerNum)
            {
                p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplySpeed(2);
                p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplyJump(2);
            }
        }
    }

    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        uses++;
        timerStarted = true;
        startTimer();
    }
}
