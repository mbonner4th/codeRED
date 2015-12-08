using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class text : MonoBehaviour {
    private Text thisText;
    public bool characters = true;
    // Use this for initialization
    void Start () {
        thisText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (characters){
            if (CharacterSelectionMenu.frost1)
            {
                thisText.text = "Don Cold:\nThe supreme leader of the resistance force against the President's hundredth term. Programs? Programmed. Projects? Managed. Beard? Grown.";
            }
            else
            {
                thisText.text = "Arthur Spiketon:\nMan or machine? Some say Spiketon's true identity is his dog, Ghost, in a machine body. The youngest of the team, he advanced to Master at age 2 with his trusty Ghost by his side. He really really loves Ghost.";
            }
        }
        else {
            if (CharacterSelectionMenu.AION)
            {
                thisText.text = "Survival: Fight against infinite evil robot professors as Don Cold. You fight until you die.";
            }
            else
            {
                thisText.text = "VSMode: Fight against another human professor for tenure. Only one can walk out alive.";
            }
        }
        
	    
	}
}
