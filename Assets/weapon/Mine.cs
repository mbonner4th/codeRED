using UnityEngine;
using System.Collections;

public class Mine : Weapon {

    public override void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
        endPoint = transform.FindChild("EndPoint");
    }

    public override void Shoot() {
        if (uses >= charges) {
            return;
        }
        uses++;
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time * 1 / effectSpawnRate;
        }
    }

    public override void Effect()
    {
        Transform btp = (Transform)Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        btp.GetComponent<MineEffect>().setDamage(damage);
        if (muzzleFlashPrefab != null) {
            Transform clone = (Transform)Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            clone.parent = firePoint;
            float size = Random.Range(0.6f, 0.9f);
            clone.localScale = new Vector3(size, size, size);
            Destroy(clone.gameObject, 0.02f);
        }
    }
}
