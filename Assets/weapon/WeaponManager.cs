using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
    private weaponSpawn[] ws;
    public float spawnCooldown = 3;

    public Transform[] commonWeapon; //common weapons
    public Transform[] uncommonWeapon; //uncommon weapons
    public Transform[] rareWeapon; // rare consumable
    public Transform[] commonConsumable; //common consumable
    public Transform[] uncommonConsumable; //uncommon consumable
    public Transform[] rareConsumable; //rare consumable

    public float commonProbability;
    public float uncommonProbability;
    public float rareProbability;
    public float WeaponVsConsumable;
    private float timeSinceSpawn;

    // Use this for initialization
    void Awake() {
        ws = FindObjectsOfType<weaponSpawn>();
        foreach(weaponSpawn w in ws)
        {
            spawn(w);
        }
        timeSinceSpawn = Time.time;
	}

    private void spawn(weaponSpawn w) {
        float r = Random.Range(0f, 1f);
        if (r < WeaponVsConsumable)
        {
            spawnWeapon(w);
        }
        else
        {
            spawnConsumable(w);
        }
    }

    private void spawnWeapon(weaponSpawn w)
    {
        Transform wTransform = w.transform;
        float r = Random.Range(0f, 1f);
        if (r < commonProbability)
        {
            int s = Random.Range(0, commonWeapon.Length);
            w.setMyWeapon((Transform)Instantiate((Transform)commonWeapon[s], wTransform.position, wTransform.rotation)); // creating first weapon
        }
        else if (r < commonProbability + uncommonProbability)
        {
            int s = Random.Range(0, uncommonWeapon.Length);
            w.setMyWeapon((Transform)Instantiate((Transform)uncommonWeapon[s], wTransform.position, wTransform.rotation)); // creating first weapon
        }
        else if (r < commonProbability + uncommonProbability + rareProbability)
        {
            int s = Random.Range(0, rareWeapon.Length);
            w.setMyWeapon((Transform)Instantiate((Transform)rareWeapon[s], wTransform.position, wTransform.rotation)); // creating first weapon
        } else
        {
            spawnWeapon(w);
        }
    }

    private void spawnConsumable(weaponSpawn w)
    {
        Transform wTransform = w.transform;
        float r = Random.Range(0f, 1f);
        if (r < commonProbability)
        {
            int s = Random.Range(0, commonConsumable.Length);
            w.setMyWeapon((Transform)Instantiate((Transform)commonConsumable[s], wTransform.position, wTransform.rotation)); // creating first weapon
        }
        else if (r < commonProbability + uncommonProbability)
        {
            int s = Random.Range(0, uncommonConsumable.Length);
            w.setMyWeapon((Transform)Instantiate((Transform)uncommonConsumable[s], wTransform.position, wTransform.rotation)); // creating first weapon
        }
        else if (r < commonProbability + uncommonProbability + rareProbability)
        {
            int s = Random.Range(0, rareConsumable.Length);
            w.setMyWeapon((Transform)Instantiate((Transform)rareConsumable[s], wTransform.position, wTransform.rotation)); // creating first weapon

        } else
        {
            spawnConsumable(w);
        }
    }

	// Update is called once per frame
	void Update () {
	    if(Time.time - timeSinceSpawn >= spawnCooldown)
        {
            ArrayList emptys = getEmptySpawners();
            float r = Random.Range(0f, 1f);
            int s = Random.Range(0, emptys.Count);
            if (emptys.Count > 0)
            {
                if (r < WeaponVsConsumable)
                {
                    spawnWeapon((weaponSpawn)emptys[s]);
                }
                else
                {
                    spawnConsumable((weaponSpawn)emptys[s]);
                }
            }
            timeSinceSpawn = Time.time;
        }
        
	}

    private ArrayList getEmptySpawners()
    {
        ArrayList emptys = new ArrayList();
        foreach (weaponSpawn w in ws) {
            if (w.isPicked())
            {
                emptys.Add(w);
            }
        }
        return emptys;
    }
}
