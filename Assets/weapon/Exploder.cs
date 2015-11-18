using UnityEngine;
using System.Collections;

public class Exploder : MonoBehaviour {
    private float startDelta;
    private float startTime;
    private float damage;

    private float explosionTime = 5;
    public Transform explosionPrefab;

    void Start() {
        startTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
	    if (Time.time - startTime + startDelta >= explosionTime) {
            Transform explosionInstance = (Transform)Instantiate(explosionPrefab,transform.position, transform.rotation);
            explosionInstance.GetComponent<Damager>().setDamage(damage);
            Destroy(this.gameObject);
        }
	}

    public void setDelta(float delta) {
        startDelta = delta;
    }

    public void setExplosionTime(float etime) {
        explosionTime = etime;
    }

    public void setDamage(float o_damage) {
        damage = o_damage;
    }
}
