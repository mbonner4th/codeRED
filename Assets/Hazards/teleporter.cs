using UnityEngine;
using System.Collections;

public class teleporter : MonoBehaviour {

    public Transform exit;
	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public Vector3 destination(Vector3 v) {
        Vector3 diff = new Vector3(transform.position.x - v.x, transform.position.y - v.y, transform.position.z - v.z);
        Vector3 dest;
        Debug.Log(diff.y);
        if (diff.x > 0)
        {
            dest = new Vector3(exit.position.x + (float)1, exit.position.y - diff.y, exit.position.z + diff.z);
        }
        else {
            dest = new Vector3(exit.position.x - (float)1, exit.position.y-diff.y, exit.position.z + diff.z);
        }
        return dest;
    }
}
