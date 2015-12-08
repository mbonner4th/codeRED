using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class chooseMode : MonoBehaviour {

    public string buttonKey;
    private Button thisButton;
    // Use this for initialization
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(buttonKey))
        {
            if (CharacterSelectionMenu.AION)
            {
                Application.LoadLevel("MapSelection");
            }
            else {
                Application.LoadLevel("characterSelection");
            }
            
        }

    }
}
