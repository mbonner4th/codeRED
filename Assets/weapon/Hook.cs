using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

    private bool isRight = false;
    private Transform myPlayer;
    private Transform myParent;
    public float amountToMove;

    void Update()
    {
        if (myParent == null)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            myPlayer.position = other.transform.position;
            if (isRight)
            {
                other.transform.Translate(-1 * amountToMove, 0, 0);
            } else
            {
                other.transform.Translate(amountToMove, 0, 0);
            }
            Destroy(this.gameObject);
        }

    }

    public void setRight(Quaternion right)
    {
        if(right.y < 1)
        {
            isRight = true;
        }
    }

    public void setMyPlayer(Transform thePlayer)
    {
        myPlayer = thePlayer;
    }

    public void setParent(Transform parent)
    {
        myParent = parent;
    }
}
