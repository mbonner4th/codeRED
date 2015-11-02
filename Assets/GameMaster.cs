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
	public Transform spawnPoint;
	public int spawnDelay = 2;

	public IEnumerator respawnPlayer(){

		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);

	}

	public static void killPlayer(Player player){
		Destroy (player.gameObject);
		gm.StartCoroutine(gm.respawnPlayer ());
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
