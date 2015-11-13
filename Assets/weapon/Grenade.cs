using UnityEngine;
using System.Collections;

public class Grenade : Weapon {


    private float throwTime;
    private bool startTimer = false;
    public float explodeTime = 5;
    // Update is called once per frame
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
        trail.Add(thrownGrenade);
        //thrownGrenade.GetComponent<ArcMove>().initialThrow();
        if (muzzleFlashPrefab != null)
        {
            Transform clone = (Transform)Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            clone.parent = firePoint;
            float size = Random.Range(0.6f, 0.9f);
            clone.localScale = new Vector3(size, size, size);
            Destroy(clone.gameObject, 0.02f);
        }
    }
}
