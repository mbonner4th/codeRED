using UnityEngine;
using System.Collections;

public class BajaBlast : Weapon
{

    private float throwTime;
    private bool startTimer = false;
    public float explodeTime = 0.5f;
    private bool isInit = false;
    private UnityStandardAssets._2D.PlatformerCharacter2D myPlayer;

    // Update is called once per frame
    void Update()
    {
        
        if (startTimer)
        {
            if (Time.time - throwTime > explodeTime)
            {
                startTimer = false;
                myPlayer = this.transform.parent.parent.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
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
        this.transform.GetComponent<AudioSource>().Play();
        startTimer = true;
        throwTime = Time.time;
    }

    public void SpeedPlayer()
    {
        myPlayer.multiplyJump(1.25f);
        myPlayer.multiplySpeed(1.25f);
        myPlayer.GetComponent<Player>().setIsSped(true);
        Debug.Log("Sped");
    }

    public override void Effect()
    {
        SpeedPlayer();
        Instantiate(BulletTrailPrefab).GetComponent<BajaBlastEffect>();
        Destroy(this.gameObject);
    }
}
