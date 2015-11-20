using UnityEngine;
using System.Collections;

public class StopWatchEffect : MonoBehaviour
{
    private int ownerNum;
    private bool timerStarted = true;
    private float timerDuration = 6.0f;
    Player[] slowedPlayers;
    bool setInstantiated = false;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!setInstantiated)
        {
            slowedPlayers = FindObjectsOfType<Player>();
            setInstantiated = true;
        }

        if(timerStarted)
        {
            if(timerDuration <= 0)
            {
                timerStarted = false;
                Debug.Log("Timer has ended");
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
        Object[] currentPlayers = FindObjectsOfType<Player>();
        foreach(Player p in currentPlayers)
        {
            foreach(Player sp in slowedPlayers)
            {
                if(p.Equals(sp) && p.playerNum != ownerNum)
                {
                    p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplySpeed(2);
                    p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplyJump(2);
                    break;
                }
            }
        }
    }

    public void setPlayerNum(int myPlayernum)
    {
        ownerNum = myPlayernum;
    }
}
