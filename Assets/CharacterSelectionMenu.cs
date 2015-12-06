using UnityEngine;
using System.Collections;

public class CharacterSelectionMenu : MonoBehaviour {

    static public bool frost1 = true;
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
}
