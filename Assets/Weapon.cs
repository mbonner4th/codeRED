using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {


	public float fireRate = 0;
	public float damage = 10;
	public LayerMask whatToHit;

	public Transform BulletTrailPrefab;
	private float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

	public Transform muzzleFlashPrefab;

	private float timeToFire = 0;
	private Transform firePoint;
    private Transform endPoint;
    // Use this for initialization
    void Awake () {
		firePoint = transform.FindChild ("FirePoint");
        endPoint = transform.FindChild("EndPoint");
        if (firePoint == null) {
		
			Debug.LogError("No Firepoint");
		}
        if (endPoint == null)
        {

            Debug.LogError("No Endpoint");
        }
    }
	
	// Update is called once per frame
	void Update () {

		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")&&this.transform.parent!=null) {//only shoot if the weapon has an owner
				Shoot ();
			}
		}
		else {
			if (Input.GetButton("Fire1") && Time.time > timeToFire && this.transform.parent != null)
            {
				timeToFire = Time.time +1/fireRate;

				Shoot();
			}
		}
	}

	void Shoot(){

		//Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
		//                                     Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
        Vector2 endPointPosition = new Vector2(endPoint.position.x, endPoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast (firePointPosition, endPointPosition - firePointPosition, 100, whatToHit);         

		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time * 1/effectSpawnRate;
		}
	//	Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition));

		if (hit.collider != null) {
		//	Debug.DrawLine(firePointPosition, hit.point, Color.red);
		//	Debug.Log("We Hit " + hit.collider.name +" and did " + damage + " Damage");
		}

	}

	void Effect(){
		Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation);
		Transform clone = (Transform)Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation);
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);

	}
}
