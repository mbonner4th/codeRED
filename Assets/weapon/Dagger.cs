using UnityEngine;
using System.Collections;

public class Dagger : Weapon {
    public override void Awake () {
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
    void Update()
    {
       

    }

    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        
        //Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
        //Vector2 endPointPosition = new Vector2(endPoint.position.x, endPoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast (firePointPosition, endPointPosition - firePointPosition, 100, whatToHit);         

        if (Time.time >= timeToSpawnEffect)
        {
            uses++;
            Effect();
            timeToSpawnEffect = Time.time * 1 / effectSpawnRate;
        }
        //	Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition));

        //if (hit.collider != null) {
        //Debug.DrawLine(firePointPosition, hit.point, Color.red);
        //Debug.Log("We Hit " + hit.collider.name +" and did " + damage + " Damage");
        //hit.collider.gameObject.GetComponent<Player>().damagePlayer((int)damage);
        //}

    }

    public override void Effect()
    {
        Transform t = (Transform)Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        t.GetComponent<Damager>().setOwner(transform.parent.parent.GetComponent<Player>().playerNum);
        t.GetComponent<Damager>().setDamage(damage);

    }
}
