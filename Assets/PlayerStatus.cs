using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    // Use this for initialization
    
    public Transform player1;
    public Transform player2;
    public static GameManager gm;

    private Text[] text;
    private Text player1lives;
    private Text player2lives;
    private Text player1weapon;
    private Text player2weapon;
    private Image[] image;
    private Image weapon1;
    private Image weapon2;
    
    void Start () {
        
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        text = gameObject.GetComponentsInChildren<Text>();
        image = gameObject.GetComponentsInChildren<Image>();
        player1lives = text[2];
        player2lives = text[3];
        player1weapon = text[4];
        player2weapon = text[5];
        weapon1 = image[2];
        weapon2 = image[3];
       


    }


    // Update is called once per frame
    void Update () {
        player1 = GameManager.gm.Frost;
        player2 = GameManager.gm.Thornton;
        player1lives.text = player1.GetComponent<Player>().lives.ToString();
        player2lives.text = player2.GetComponent<Player>().lives.ToString();
        if (player1 != null && player1.GetComponent<Player>().getWeapon() != null)
        {
            weapon1.sprite = player1.GetComponent<Player>().getWeapon().GetComponent<SpriteRenderer>().sprite;
            player1weapon.text = player1.GetComponent<Player>().getWeapon().GetComponent<Weapon>().getChargesleft().ToString();
        }
        else {
            weapon1.sprite = null;
            player1weapon.text = null;
        }
        if (player2 != null && player2.GetComponent<Player>().getWeapon() != null)
        {
            weapon2.sprite = player2.GetComponent<Player>().getWeapon().GetComponent<SpriteRenderer>().sprite;
            player2weapon.text = player2.GetComponent<Player>().getWeapon().getChargesleft().ToString();
        }
        else {
            weapon2.sprite = null;
            player2weapon.text = null;
        }

    }
}
