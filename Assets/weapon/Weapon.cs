using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {


	public float fireRate = 0;
	public float damage = 10;
	public LayerMask whatToHit;
	
	public Transform BulletTrailPrefab;
	protected float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;
    public int charges = 5;
    protected int uses = 0;
	public Transform muzzleFlashPrefab;

	protected float timeToFire = 0;
	protected Transform firePoint;
    protected Transform endPoint;
    // Use this for initialization
    public virtual void Awake () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    public virtual void Release()
    {

    }

	public virtual void Shoot(){


	}

	public virtual void Effect(){


	}
    public int getChargesleft() {
        return charges - uses;
    }

    public void destroyWeapon()
    {
        Destroy(this.gameObject);
    }
}
