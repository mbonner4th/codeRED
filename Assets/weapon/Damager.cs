using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour {

    private float damage;
    private bool hasOwner = false;
    private int owner = 0;
    private bool hitStructures = false;
	// Update is called once per frame
	void Update () {
       
	}

    public void setDamage(float o_damage) {
        damage = o_damage;
    }
    public void setOwner(int i) {
        hasOwner = true;
        owner = i;
    }
    public void setStructures(bool b) {
        hitStructures = b;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if (hasOwner && other.GetComponent<Player>().playerNum != owner)
            {
                other.GetComponent<Player>().damagePlayer((int)damage);
                Destroy(this.gameObject);

            } else if (hasOwner == false) {
                other.GetComponent<Player>().damagePlayer((int)damage);
                Destroy(this.gameObject);
            }
        } else if (other.gameObject.tag == "Structures") {
            if (!hitStructures) {
                Destroy(this.gameObject);
            }
        }
    }


}
