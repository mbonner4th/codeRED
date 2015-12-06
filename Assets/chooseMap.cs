using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class chooseMap : MonoBehaviour {


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
           Application.LoadLevel(transform.parent.FindChild("highlight").GetComponent<highlight>().moveTo.name); 
        }

    }
}

