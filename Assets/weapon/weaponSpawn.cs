using UnityEngine;
using System.Collections;

public class weaponSpawn : MonoBehaviour {

    private Transform myWeapon = null;
    private bool pickedUp = false;
    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
        picked();
    }

    void picked() {
        if (pickedUp == false && myWeapon.parent != null) {
            pickedUp = true;  //reset timer if a weapon is picked up
        }
    }

    public void setMyWeapon(Transform weapon)
    {
        myWeapon = weapon;
        pickedUp = false;
    }

    public bool isPicked()
    {
        return pickedUp;
    }
}
