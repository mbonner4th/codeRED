using UnityEngine;
using System.Collections;

public class Sword : Weapon {

    private bool pickedUp = false;
    private bool shooting = false;
    public float stopSwingAngle;
    public float stopSwingTime;
    private float startTime;
    private Vector3 swingDifference;

    public override void Awake() {
        float timeDiff = Time.deltaTime;
        swingDifference.x = 0;
        swingDifference.y = 0;
        swingDifference.z = -1*(75 - stopSwingAngle)/stopSwingTime*timeDiff;
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
            Vector3 rotate90down;
            rotate90down.x = 0;
            rotate90down.y = 0;
            rotate90down.z = -90;
            transform.Rotate(rotate90down);
            Vector3 rotateUp;
            rotateUp.x = 0;
            rotateUp.y = 0;
            rotateUp.z = 75;
            transform.parent.Rotate(rotateUp);
            uses++;
            shooting = true;
            startTime = Time.time;
        }
    }

    public override void Effect() {
        transform.parent.Rotate(swingDifference);
        if(Time.time - startTime >= stopSwingTime) {
            shooting = false;
            Quaternion armRotation;
            if (transform.parent.localRotation.w > 0.5) { //Facing right
                armRotation.w = 1;
                armRotation.x = 0;
                armRotation.y = 0;
                armRotation.z = 0;
            } else {
                armRotation.w = 0;
                armRotation.x = 0;
                armRotation.y = 1;
                armRotation.z = 0;
            }
            transform.parent.rotation = armRotation;
            Vector3 rotate90;
            rotate90.x = 0;
            rotate90.y = 0;
            rotate90.z = 90;
            transform.Rotate(rotate90);
            this.transform.GetComponent<AudioSource>().Play();
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
