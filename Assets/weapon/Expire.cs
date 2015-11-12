using UnityEngine;
using System.Collections;

public class Expire : MonoBehaviour {

    public double expireTime = 0.5;

    private double startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time - startTime >= expireTime) {
            Destroy(this.gameObject);
        }
	}
}
