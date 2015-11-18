using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour {

    private float damage;

	// Update is called once per frame
	void Update () {
        Object[] o = FindObjectsOfType<Player>();
	    foreach(Player p in o) {
            if (Physics2D.IsTouching(p.GetComponent<Collider2D>(), this.GetComponent<Collider2D>())) {
                p.damagePlayer((int)damage);
            }
        }
	}

    public void setDamage(float o_damage) {
        damage = o_damage;
    }

}
