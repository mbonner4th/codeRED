using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
    private bool paused;

    public bool Paused {
        get {
            return paused;
        }
    }

	public static GameMaster gm;

	void Start(){
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
    public Transform player2Prefab;
	public Transform spawnPoint;
    public Transform spawnPoint2;
	public int spawnDelay = 2;
    public int numPlayers = 2;

	public IEnumerator respawnPlayer(int num_Player){

		yield return new WaitForSeconds (spawnDelay);

		if(num_Player == 1)
            Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
        else if(num_Player == 2)
            Instantiate(player2Prefab, spawnPoint2.position, spawnPoint2.rotation);

    }

	public static void killPlayer(Player player){
		Destroy (player.gameObject);
		gm.StartCoroutine(gm.respawnPlayer (player.playerNum));
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
