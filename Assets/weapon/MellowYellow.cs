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
        Instantiate(BulletTrailPrefab).GetComponent<MellowYellowEffect>();
        Destroy(this.gameObject);
        GameManager.killPlayer(transform.parent.parent.GetComponent<Player>());
    }
}
