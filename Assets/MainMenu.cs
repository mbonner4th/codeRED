using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void StartGame(string level) {
        Application.LoadLevel(level);
    }
}
