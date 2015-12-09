using UnityEngine;
using System.Collections;

public class getStumpped : Weapon{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Effect()
    {


        Object[] o = FindObjectsOfType(typeof(Player));
        foreach (Player p in o)
        {
            if (p.playerNum != this.transform.parent.parent.GetComponent<Player>().playerNum)
            {
                GameManager.killPlayer(p);
            }
        }
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
