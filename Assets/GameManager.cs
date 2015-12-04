using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager gm;

    public bool debugMode = false;
    public Transform playerPrefab;
    public Transform player2Prefab;
    public GameObject[] spawnPoints;
    private float spawnPointRange; //need this for randomly getting spawn point
    public Transform Jail;

    public int respawnDelay = 1;

	// Use this for initialization
	void Start () {
	    playerPrefab.GetComponent<Player>().setPlayerNum(1);
        player2Prefab.GetComponent<Player>().setPlayerNum(2);
        spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawner");
        
        spawnPointRange = (float)spawnPoints.Length;
        Debug.Log(spawnPointRange);
	}
     void Awake(){
         gm = this;
     }

     public IEnumerator respawnPlayer(Player player)
     {

         yield return new WaitForSeconds(respawnDelay);
         Renderer[] rs = player.GetComponentsInChildren<Renderer>();
         foreach (Renderer r in rs)
         {
             r.enabled = true;
         }
         int randomNumbSpawn = Mathf.FloorToInt(Random.Range(0.1f, spawnPointRange));
         Debug.Log("respawning at: " + randomNumbSpawn);
         player.transform.position = spawnPoints[randomNumbSpawn].transform.position;
         //Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
     }

     public static void killPlayer(Player player)
     {

        
         Debug.Log("Player is dead!");
         --player.lives;
         
         player.transform.position = gm.Jail.position;
         if (player.transform.FindChild("arm").childCount == 2)
         {
             Destroy(player.transform.FindChild("arm").GetChild(1).gameObject);
         }
         //player.transform.position = gm.gameObject.transform.GetChild(0).position;
         Renderer[] rs = player.GetComponentsInChildren<Renderer>();
         foreach(Renderer r in rs){
            r.enabled = false;
         }
         //player.turnInvincible(gm.respawnDelay);
         if (player.lives >= 0)
         {
             gm.StartCoroutine(gm.respawnPlayer(player));
         }
         
         
     }
	
	// Update is called once per frame
	void Update () {
	
	}
}