using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager gm;

    public bool debugMode = false;
    public Transform FrostPrefab;
    public Transform Frost;
    public Transform ThorntonPrefab;
    public Transform Thornton;
    public Transform[] spawnPoints;
    private float spawnPointRange; //need this for randomly getting spawn point
    public Transform Jail;

    public int respawnDelay = 1;

	// Use this for initialization
	void Start () {
        Debug.Log(CharacterSelectionMenu.frost1);

       
    }
    void Awake(){
        gm = this;
        spawnPointRange = (float)spawnPoints.Length;
        if (CharacterSelectionMenu.frost1 == true)
        {
            FrostPrefab.GetComponent<Player>().setPlayerNum(1);
            ThorntonPrefab.GetComponent<Player>().setPlayerNum(2);
        }
        else
        {
            FrostPrefab.GetComponent<Player>().setPlayerNum(2);
            ThorntonPrefab.GetComponent<Player>().setPlayerNum(1);
        }
        Frost = (Transform)Instantiate(FrostPrefab, spawnPoints[Mathf.FloorToInt(Random.Range(0.1f, spawnPointRange))].position, FrostPrefab.rotation);
        Thornton = (Transform)Instantiate(ThorntonPrefab, spawnPoints[Mathf.FloorToInt(Random.Range(0.1f, spawnPointRange))].position, ThorntonPrefab.rotation);
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
         player.transform.position = spawnPoints[randomNumbSpawn].position;
         //Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
     }

     public static void killPlayer(Player player)
     {

        
         Debug.Log("Player is dead!");
         --player.lives;
         
         player.transform.position = gm.Jail.position;
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