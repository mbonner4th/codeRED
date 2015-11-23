using UnityEngine;
using System.Collections;

public class Sword : Weapon {

    private bool pickedUp = false;
    private bool shooting = false;
    public float stopSwingAngle;
    public float stopSwingTime;
    private float startTime;
    private Vector3 swingDifference;
    private Quaternion startRotation;

    void Awake() {
        float timeDiff = Time.deltaTime;
        swingDifference.x = 0;
        swingDifference.y = 0;
        swingDifference.z = -1*(90 - stopSwingAngle)/stopSwingTime*timeDiff;
    }

	// Update is called once per frame
	void Update () {
        if (!pickedUp) {
            if(transform.parent != null) {
                pickedUp = true;
                Vector3 rotate90;
                rotate90.x = 0; rotate90.y = 0; rotate90.z = 90;
                transform.Rotate(rotate90);
            }
        }
        if (shooting) {
            Effect();
            dealDamage();
        }
	}

    public override void Shoot() {
        if (uses == charges) { return; }
        if (!shooting) {
            startRotation = transform.rotation;
            uses++;
            shooting = true;
            startTime = Time.time;
        }
    }

    public override void Effect() {
        transform.Rotate(swingDifference);
        if(Time.time - startTime >= stopSwingTime) {
            shooting = false;
            transform.rotation = startRotation;
        }
    }

    private void dealDamage() {
        Player[] players = FindObjectsOfType<Player>();
        foreach(Player p in players) {
            if(Physics2D.IsTouching(p.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>()))
            {
                p.damagePlayer((int)damage);
            }
        }
    }
}
