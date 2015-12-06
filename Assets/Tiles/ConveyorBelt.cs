using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

    public bool isRight;
    public float speed;

    void Awake()
    {
        if (!isRight)
        {
            speed *= -1;
        }
    }

    void Update()
    {
        
    }

	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 v = other.transform.position;
            v.x += speed;
            other.transform.position = v;
        }
    }
}
