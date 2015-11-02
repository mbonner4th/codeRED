using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats{
		public int Health = 100;
	}

	public PlayerStats playerStats = new PlayerStats();
	public int fallBoundary = -20;
    private Transform arm; //players'arm
    private Weapon myWeapon = null;
    private Transform weaponPoint; //where to put the weapon
	void Update(){
		if (transform.position.y <= fallBoundary)
			damagePlayer (1000);
        if (!GameMaster.gm.Paused) {
            if (Input.GetButtonDown("Grab1"))
            {//currently using the default fire 2 button, can change to some keys later
                pickUp();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
            }
        }
	}

	public void damagePlayer(int damage){
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			Debug.Log("Player Is Kill");
			GameMaster.killPlayer(this);
		}
	}

    public void shoot() {
        if (myWeapon != null) {
            myWeapon.Shoot();
        }
    }

    public void pickUp() {
        arm = transform.FindChild("arm");
        weaponPoint = arm.FindChild("weaponPoint");
        Object[] o = FindObjectsOfType(typeof(Weapon));//look for all weapons on the map
        foreach (Weapon n in o) {
            if (Physics2D.IsTouching(this.GetComponent<Collider2D>(), 
                        ((Weapon)n).GetComponent<Collider2D>())) { //check is the player touching any weapon
                if (arm.childCount>1) {// Destroy the old weapon in arm
                    Destroy(arm.GetChild(1).gameObject);
                    myWeapon = null;

                }
                n.transform.SetParent(arm);  // attach new weapon to arm
                Vector2 weaponPoistion = new Vector2(weaponPoint.position.x, weaponPoint.position.y);
                n.transform.position = weaponPoistion;
                n.transform.rotation = arm.rotation;
                myWeapon = (Weapon)n;
                break;
            }
        }

    }




}
