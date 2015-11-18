using UnityEngine;
using System.Collections;

public class MellowYellow : Weapon {

    // Update is called once per frame
    void Update()
    {

    }



    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        Effect();
    }

    public override void Effect()
    {
        Destroy(this.gameObject);
        GameMaster.killPlayer(transform.parent.parent.GetComponent<Player>());
    }
}
