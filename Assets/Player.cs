using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats{
		public int Health = 100;
	}

	public PlayerStats playerStats = new PlayerStats();
	public int fallBoundary = -20;

	void Update(){
		if (transform.position.y <= fallBoundary)
			damagePlayer (1000);
	}

	public void damagePlayer(int damage){
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			Debug.Log("Player Is Kill");
			GameMaster.killPlayer(this);
		}
	}



}
