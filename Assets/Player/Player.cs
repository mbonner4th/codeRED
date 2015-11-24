using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats{
		public int Health = 1;
	}

	public PlayerStats playerStats = new PlayerStats();
	public int fallBoundary = -20;
    public int playerNum;
    public int lives;
    public int invinicbleDuration = 2;

    private float invincibleTime;
    private bool invincible = false;
    private Transform arm; //players'arm
    private Weapon myWeapon = null;
    private Transform weaponPoint; //where to put the weapon
    private Animator m_Anim;            // Reference to the player's animator component.

    private void Awake(){
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("isKill", false);
        turnInvincible(2);
    }

	void Update(){
        if (invincible && (Time.time-invinicbleDuration >invincibleTime)) {
            invincible = false;
        }
        if (transform.position.y <= fallBoundary)
            GameMaster.killPlayer(this);
        if (GameMaster.gm)
        {
            if (!GameMaster.gm.Paused)
            {
                if (playerNum == 1)
                {
                    if (Input.GetButtonDown("Grab1"))
                    {//currently using the default fire 2 button, can change to some keys later
                        pickUp();
                    }
                    if (Input.GetButtonDown("Fire1"))
                    {
                        shoot();
                    }
                    else if (Input.GetButtonUp("Fire1"))
                    {
                        release();
                    }
                }
                else if (playerNum == 2)
                {
                    if (Input.GetButtonDown("Grab2"))
                    {//currently using the default fire 2 button, can change to some keys later
                        pickUp();
                    }
                    if (Input.GetButtonDown("Fire2"))
                    {
                        shoot();
                    }
                    else if (Input.GetButtonUp("Fire2"))
                    {
                        release();
                    }
                }
            }
        }
	}

   void OnTriggerEnter2D(Collider2D other)
    {
 
        if(other.gameObject.tag == "Spikes")
        {

            damagePlayer(1000);
        }
        if (other.gameObject.tag == "teleporter") {

            transform.position = other.gameObject.GetComponent<teleporter>().destination(transform.position);
        }
    }

	public void damagePlayer(int damage){
        if (!invincible) {
            playerStats.Health -= damage;
            if (playerStats.Health <= 0)
            {
                Debug.Log("Player Is Kill");
                m_Anim.SetBool("isKill", true);
                GameMaster.killPlayer(this);
            }
        }
	}

    public void release() {
        if (myWeapon != null) {
            myWeapon.Release();
        }
    }

    public void shoot() {
        if (myWeapon != null) {
            myWeapon.Shoot();
        }
    }

    public void turnInvincible(int duration) {
        invincible = true;
        invinicbleDuration = duration;
        invincibleTime = Time.time;
    }
    public void pickUp() {
        arm = transform.FindChild("arm");
        weaponPoint = arm.FindChild("weaponPoint");
        Object[] o = FindObjectsOfType(typeof(Weapon));//look for all weapons on the map
        foreach (Weapon n in o) {
            if (Physics2D.IsTouching(this.GetComponent<BoxCollider2D>(), 
                        ((Weapon)n).GetComponent<Collider2D>())) { //check is the player touching any weapon
                if (arm.childCount>1) {// Destroy the old weapon in arm
                    Debug.Log("touching");
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
