using UnityEngine;
using System.Collections;

public class GrapplingHook : Weapon {

    private Transform bullet = null;
    private bool shooting = false;
    private float theX;

    public override void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet == null)
        {
            shooting = false;
        }
        if (shooting)
        {
            Vector3 r = transform.parent.parent.position;
            r.x = theX;
            transform.parent.parent.position = r;
        }
        
    }

	public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        if (bullet == null)
        {
            uses++;
            Effect();
            shooting = true;
            theX = transform.parent.parent.position.x;
        }
    }

    public override void Effect()
    {
        bullet = (Transform)Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Hook>().setRight(firePoint.rotation);
        bullet.GetComponent<Hook>().setMyPlayer(transform.parent.parent);
        bullet.GetComponent<Hook>().setParent(transform);
    }
}
