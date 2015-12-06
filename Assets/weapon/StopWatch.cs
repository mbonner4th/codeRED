using UnityEngine;
using System.Collections;

public class StopWatch : Weapon {
    
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
         
	}

    void slowPlayers()
    {
        Object[] o = FindObjectsOfType(typeof(Player));
        foreach(Player p in o)
        {
            if(p.playerNum != this.transform.parent.parent.GetComponent<Player>().playerNum)
            {
                p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplySpeed(0.5f);
                p.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplyJump(0.5f);
                p.setIsSlowed(true);
            }
        }
    }

    public override void Effect()
    {
        slowPlayers();
        Instantiate(BulletTrailPrefab).GetComponent<StopWatchEffect>();
        Destroy(this.gameObject);
    }

    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        uses++;
        Effect();
    }
}
