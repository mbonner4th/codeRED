using UnityEngine;
using System.Collections;

public class Swamp : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplyJump((float)System.Math.Pow(0.5,1.0/3.0));
            other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplySpeed((float)System.Math.Pow(0.25, 1.0 / 3.0));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplyJump((float)System.Math.Pow(2.0, 1.0 / 3.0));
            other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().multiplySpeed((float)System.Math.Pow(4.0, 1.0 / 3.0));
        }
    }
}
