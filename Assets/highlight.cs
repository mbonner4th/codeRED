using UnityEngine;
using System.Collections;

public class highlight : MonoBehaviour {
    public Transform[] choices;
    public Transform moveTo;
    private int current;
    void Start() {
        moveTo = choices[0];
    }
    void Update() {
        if (Input.GetKeyDown("a") && current > 0) {
            current -= 1;
            moveTo = choices[current];
            transform.position = moveTo.position;
        } else if (Input.GetKeyDown("d") && current < choices.Length-1) {
            current += 1;
            moveTo = choices[current];
            transform.position = moveTo.position;
        }
    }


}
