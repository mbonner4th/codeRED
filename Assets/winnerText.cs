using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class winnerText : MonoBehaviour
{
    private Text thisText;
    private int winner = 0;
    // Use this for initialization
    void Start()
    {
        thisText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (winner == 1) {
            thisText.text = "Winner is player 1";
        } else if (winner == 2) {
            thisText.text = "Winner is player 2";
        }
        
    }
    public void setWinner(int win) {
        winner = win;
    }
}
