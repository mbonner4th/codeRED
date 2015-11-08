using UnityEngine;
using System.Collections;

public class weaponSpawn : MonoBehaviour {
    public Transform dagger;
    public int spawnCooldown = 3;

    private ArrayList commonWeapon = new ArrayList(); //common weapons
    //private ArrayList uncommonWeapon = new ArrayList(); uncommon weapons
    //private ArrayList rareWeapon = new ArrayList(); rare wapons
    private Transform myWeapon = null;
    private float timeSinceLastSpawn;
    private bool pickedUp = false;
    // Use this for initialization
    void Awake () {

        commonWeapon.Add(dagger);//adding weapon prefabs to the list
        int s = Random.Range(0, commonWeapon.Count);
        myWeapon = (Transform)Instantiate ((Transform)commonWeapon[s],transform.position, transform.rotation); // creating first weapon
        timeSinceLastSpawn = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        picked();
        if (pickedUp == true && (Time.time > timeSinceLastSpawn+spawnCooldown)) {
            /*  save for later use when we have different weapon rarity
            int r = Random.Value();
            if (r < probabilityOfCommon) {
            }
            else if (r < probabilityOfUncommon) {
            }
            else (){
            }
            */
            int s = Random.Range(0, commonWeapon.Count);
            myWeapon = (Transform)Instantiate((Transform)commonWeapon[s], transform.position, transform.rotation);
            pickedUp = false;
        }
    }
    void picked() {
        if (pickedUp == false && myWeapon.parent != null) {
            pickedUp = true;  //reset timer if a weapon is picked up
            timeSinceLastSpawn = Time.time;
        }
    }
}
