using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    // Use this for initialization
    public Text[] text;
    public Text player1lives;
    public Text player2lives;
    public Text player1weapon;
    public Text player2weapon;
    public Image[] image;
    public Image weapon1;
    public Image weapon2;
    public Transform player1;
    public Transform player2;
    public static GameMaster gm;
	void Start () {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
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
