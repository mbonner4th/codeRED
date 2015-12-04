using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon {
    private Transform myBullet = null;
    private bool shooting = false;
    private float theX;

    public override void Awake() {
        firePoint = transform.FindChild("FirePoint");
    }


    void Update()
    {
        if (shooting)
        {
            Vector3 v = transform.parent.parent.position;
            v.x = theX;
            transform.parent.parent.position = v;
        }
        if (myBullet == null)
        {
            shooting = false;
        }
    }

    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        if (myBullet == null)
        {
            shooting = true;
            uses++;
            Effect();
            theX = transform.parent.parent.position.x;
        }
    }

    public override void Effect()
    {
        myBullet = (Transform)Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        myBullet.GetComponent<ControlledMovement>().setPlayerNum(transform.parent.parent.GetComponent<Player>().playerNum);
        myBullet.GetComponent<ControlledMovement>().setParent(transform);
        myBullet.GetComponent<Damager>().setDamage(damage);
    }
}
