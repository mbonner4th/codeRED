using UnityEngine;
using System.Collections;

public class Grenade : Weapon {


    private float throwTime;
    private bool startTimer = false;
    public float explodeTime = 5;
    // Update is called once per frame
    public override void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
        endPoint = transform.FindChild("EndPoint");
        if (firePoint == null)
        {

            Debug.LogError("No Firepoint");
        }
        if (endPoint == null)
        {

            Debug.LogError("No Endpoint");
        }
    }
    void Update()
    {
        if (startTimer)
        {
            if (Time.time - throwTime > explodeTime) {
                startTimer = false;
                Effect();
            }
        }

    }

    public override void Release()
    {
        if (startTimer) {
            startTimer = false;
            Effect();
        }
    }

    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        uses++;
        startTimer = true;
        throwTime = Time.time;
    }

    public override void Effect()
    {
        Transform thrownGrenade = (Transform)Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        thrownGrenade.gameObject.GetComponent<ArcMove>().initialThrow(firePoint.rotation);
        Exploder thrownGrenadeExploder = thrownGrenade.GetComponent<Exploder>();
        thrownGrenadeExploder.setDelta(Time.time - throwTime);
        thrownGrenadeExploder.setExplosionTime(explodeTime);
        thrownGrenadeExploder.setDamage(damage);
        //thrownGrenade.GetComponent<ArcMove>().initialThrow();

    }
}
