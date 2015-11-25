using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
    private bool paused;
    public Transform playerPrefab;
    public Transform player2Prefab;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform player1;
    public Transform player2;
    public int spawnDelay = 2;
    public int numPlayers = 2;
    public int player1lives = 5;
    public int player2lives = 5;
    public bool Paused {
        get {
            return paused;
        }
    }

	public static GameMaster gm;

	void Start(){
        Debug.Log("gm is made here");
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster>();
		}
        
    }

    
    public IEnumerator respawnPlayer(int num_Player,int lives){

		yield return new WaitForSeconds (spawnDelay);
        Debug.Log("spawn");
        if (num_Player == 1)
        {
            Debug.Log("spawn1");
            Transform p = (Transform)Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            player1 = p;
            p.GetComponent<Player>().lives = lives - 1;
            player1lives -= 1;
        }

        else if (num_Player == 2) {
            Transform p = (Transform)Instantiate(player2Prefab, spawnPoint2.position, spawnPoint2.rotation);
            player2 = p;
            p.GetComponent<Player>().lives = lives - 1;
            player2lives -= 1;

        }


    }

	public static void killPlayer(Player player){
		Destroy (player.gameObject);
        if (player.lives>0) {
            gm.StartCoroutine(gm.respawnPlayer(player.playerNum,player.lives));
        }
		
	}

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            Pause();
            if (paused) {
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        }
    }

    public void Pause() {
        paused = !paused;
    }
}
