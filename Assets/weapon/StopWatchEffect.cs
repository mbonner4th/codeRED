using UnityEngine;
using System.Collections;

public class StopWatchEffect : MonoBehaviour
{
    private bool timerStarted = true;
    private float timerDuration = 6.0f;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update ()
    {
        

        if(timerStarted)
        {
            if(timerDuration <= 0)
            {
                timerStarted = false;
                Debug.Log("Timer has ended");
                speedPlayers();
                Destroy(this.gameObject);
            }
            else
            {
                timerDuration -= Time.deltaTime;
            }
        }

	
	}

    void speedPlayers()
    {
        Object[] currentPlayers = FindObjectsOfType<Player>();
        foreach(Player p in currentPlayers)
        {
                if(p.getIsSlowed())
                {
                    p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplySpeed(2);
                    p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplyJump(2);
                    p.setIsSlowed(false);
                    break;
                }
        }
    }

}
