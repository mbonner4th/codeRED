using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats{
		public int Health = 100;
	}

	public PlayerStats playerStats = new PlayerStats();
	public int fallBoundary = -20;
    private Transform arm;
	void Update(){
		if (transform.position.y <= fallBoundary)
			damagePlayer (1000);
        if (Input.GetButtonDown("Fire2")) {//currently using the default fire 2 button, can change to some keys later
            pickUp();
        }
	}

	public void damagePlayer(int damage){
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			Debug.Log("Player Is Kill");
			GameMaster.killPlayer(this);
		}
	}
    

    public void pickUp() {
        arm = transform.FindChild("arm");
        Object[] o = FindObjectsOfType(typeof(Weapon));//look for all weapons on the map
        foreach (Weapon n in o) {
            if (Physics2D.IsTouching(this.GetComponent<Collider2D>(), 
                        ((Weapon)n).GetComponent<Collider2D>())) { //check is the player touching any weapon
                if (arm.childCount>0) {// Destroy the old weapon in arm
                    Destroy(arm.GetChild(0).gameObject);
                }
                n.transform.parent = transform.FindChild("arm");  // attach new weapon to arm
                break;
            }
        }

    }




}
