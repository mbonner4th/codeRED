using UnityEngine;
using System.Collections;

public class CharacterSelectionMenu : MonoBehaviour {

    static public bool frost1 = true;
    static public bool AION = false;
    public void StartGame(string level)
    {
        Application.LoadLevel(level);
    }
    public void SelectCharacter(string name)
    {
        if (name == "Frost")
        {
            frost1 = true;
        }
        else
        {
            frost1 = false;
        }
    }
    public void SelectMode(string name) {
        if (name == "survival")
        {
            AION = true;
        }
        else
        {
            AION = false;
        }
    }
}
