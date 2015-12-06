using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class text : MonoBehaviour {
    private Text thisText;
    // Use this for initialization
    void Start () {
        thisText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (CharacterSelectionMenu.frost1)
        {
            thisText.text = "Current selection : Frost";
        }
        else {
            thisText.text = "Current selection : Thornton";
        }
	    
	}
}
