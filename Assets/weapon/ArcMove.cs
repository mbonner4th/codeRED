using UnityEngine;
using System.Collections;

public class ArcMove : MonoBehaviour {
    public float initialX = 10;
    public float initialY = 20;

	public void initialThrow(Quaternion rotation)
    {
        float xmult = 1;
        if (rotation.w != 1.0)
        {
            xmult = -1;
        }
        Vector2 first;
        first.x = initialX * xmult;
        first.y = initialY;
        GetComponent<Rigidbody2D>().velocity = first;
    }
}
