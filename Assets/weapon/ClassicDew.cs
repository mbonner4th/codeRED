using UnityEngine;
using System.Collections;

public class ClassicDew : Weapon {

    private float throwTime;
    private bool startTimer = false;
    public float explodeTime = 1;
    // Update is called once per frame

    void Update()
    {
        if (startTimer)
        {
            if (Time.time - throwTime > explodeTime)
            {
                startTimer = false;
                Effect();
            }
        }

    }

    public override void Release()
    {
        if (startTimer)
        {
            startTimer = false;
        }
    }

    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        startTimer = true;
        throwTime = Time.time;
    }

    public override void Effect()
    {
        if (transform.parent.parent.GetComponent<Player>().lives < 5) {
            transform.parent.parent.GetComponent<Player>().lives += 1;
        }
        Destroy(this.gameObject);
    }
}
