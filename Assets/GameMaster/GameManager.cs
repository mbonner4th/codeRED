using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager gm;

    public bool debugMode = false;
    public Transform FrostPrefab;
    public Transform Frost;
    public Transform ThorntonPrefab;
    public Transform Thornton;
    public GameObject winnerScreen;
    public GameObject pauseMenu;
    private bool isPause = false;
    private GameObject[] spawnPoints;
    private float spawnPointRange; //need this for randomly getting spawn point
    public Transform Jail;

    public int respawnDelay = 1;

	// Use this for initialization
	void Start () {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        gm.winnerScreen.SetActive(false);
    }
    void Awake(){
        // gm = this;
        Time.timeScale = 1;
        spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawner");
        spawnPointRange = (float)spawnPoints.Length;
        if (CharacterSelectionMenu.frost1 == true){
            FrostPrefab.GetComponent<Player>().setPlayerNum(1);
            ThorntonPrefab.GetComponent<Player>().setPlayerNum(2);
        }else{
            FrostPrefab.GetComponent<Player>().setPlayerNum(2);
            ThorntonPrefab.GetComponent<Player>().setPlayerNum(1);
        }

        Frost = (Transform)Instantiate(FrostPrefab, spawnPoints[Mathf.FloorToInt(Random.Range(0.1f, spawnPointRange))].transform.position, FrostPrefab.rotation);
        Thornton = (Transform)Instantiate(ThorntonPrefab, spawnPoints[Mathf.FloorToInt(Random.Range(0.1f, spawnPointRange))].transform.position, ThorntonPrefab.rotation);
        if (CharacterSelectionMenu.AION)
        {
            Thornton.GetComponent<Player>().lives = 0;
            //gm.winnerScreen.GetComponentInChildren<Text>().GetComponent<winnerText>().setScore(0);
        }
    }
    


    public IEnumerator respawnPlayer(Player player)
     {

         yield return new WaitForSeconds(respawnDelay);
         //Renderer[] rs = player.GetComponentsInChildren<Renderer>();
         //foreach (Renderer r in rs)
         //{
         //    r.enabled = true;
         //}
         int randomNumbSpawn = Mathf.FloorToInt(Random.Range(0.1f, spawnPointRange));
         //Debug.Log("respawning at: " + randomNumbSpawn);
         player.transform.position = spawnPoints[randomNumbSpawn].transform.position;
         player.turnInvincible(2);
        player.IsDead = false;
         //Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
     }

     public static void killPlayer(Player player)
     {

        --player.lives;
        //Debug.Log("Player is dead!");
        if (CharacterSelectionMenu.AION) {
            if(player.playerNum == 2)
            {
                player.lives += 2;
            }
        }
        //gm.winnerScreen.GetComponentInChildren<Text>().GetComponent<winnerText>().setScore(player.lives);



        player.transform.position = gm.Jail.position;
         if (player.transform.FindChild("arm").childCount == 2)
         {
             Destroy(player.transform.FindChild("arm").GetChild(1).gameObject);
         }
        //player.transform.position = gm.gameObject.transform.GetChild(0).position;
        //Renderer[] rs = player.GetComponentsInChildren<Renderer>();
        //foreach(Renderer r in rs){
        // r.enabled = false;
        //}
        //player.turnInvincible(gm.respawnDelay);
		player.IsDead = true;
        if (player.lives > 0)
        {
            gm.StartCoroutine(gm.respawnPlayer(player));
        }
        else {
            Time.timeScale = 0;
            gm.winnerScreen.SetActive(true);
 
            if (player.playerNum == 1) {
                gm.winnerScreen.GetComponentInChildren<Text>().GetComponent<winnerText>().setWinner(2);
            }
            else {
                gm.winnerScreen.GetComponentInChildren<Text>().GetComponent<winnerText>().setWinner(1);
            }




        }


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}