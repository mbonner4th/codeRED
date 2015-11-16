using UnityEngine;
using System.Collections;

public class CodeRed : Weapon {


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
        transform.parent.parent.GetComponent<Player>().turnInvincible(10);
    }
}