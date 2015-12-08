﻿using UnityEngine;
using System.Collections;

public class MellowYellow : Weapon {

    // Update is called once per frame
    void Update()
    {

    }



    public override void Shoot()
    {
        if (uses == charges)
        {
            return;
        }
        this.transform.GetComponent<AudioSource>().Play();
        Effect();
    }

    public override void Effect()
    {
        Destroy(this.gameObject);
        GameManager.killPlayer(transform.parent.parent.GetComponent<Player>());
    }
}
