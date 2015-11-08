using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {


	public float fireRate = 0;
	public float damage = 10;
	public LayerMask whatToHit;
	
	public Transform BulletTrailPrefab;
	private float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;
    public int charges = 5;
    private int uses = 0;
	public Transform muzzleFlashPrefab;

	private float timeToFire = 0;
	private Transform firePoint;
    private Transform endPoint;
	private ArrayList trail = new ArrayList();
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
		if(trail.Count>0){
			Object[] o = FindObjectsOfType(typeof(Player));
			
			for(int i = 0; i<trail.Count;i++){
				Transform t = (Transform)trail[i];
				if(t!=null){
					foreach (Player p in o) {
						
						if(Physics2D.IsTouching(((Transform)t).GetComponent<Collider2D>(),
								((Player)p).GetComponent<Collider2D>())){
								Debug.Log(p);
							((Player)p).GetComponent<Player>().damagePlayer((int)damage);
							Destroy(((Transform)t).gameObject);
						}
					}
				}
				
			}
			
		}

		
	}

	public void Shoot(){
        if (uses == charges) {
            return;
        }
        uses++;
		//Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
        //Vector2 endPointPosition = new Vector2(endPoint.position.x, endPoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast (firePointPosition, endPointPosition - firePointPosition, 100, whatToHit);         

		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time * 1/effectSpawnRate;
		}
	//	Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition));

		//if (hit.collider != null) {
			//Debug.DrawLine(firePointPosition, hit.point, Color.red);
			//Debug.Log("We Hit " + hit.collider.name +" and did " + damage + " Damage");
			//hit.collider.gameObject.GetComponent<Player>().damagePlayer((int)damage);
		//}

	}

	void Effect(){
		trail.Add((Transform)Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation));
        if (muzzleFlashPrefab!=null) {
            Transform clone = (Transform)Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            clone.parent = firePoint;
            float size = Random.Range(0.6f, 0.9f);
            clone.localScale = new Vector3(size, size, size);
            Destroy(clone.gameObject, 0.02f);
        }


	}
}
