using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats{
		public int Health = 1;
	}

	public PlayerStats playerStats = new PlayerStats();
	public int fallBoundary = -20;
    public int playerNum;
    public int lives;
	public int invinicbleDuration = 2;
	public string Path = "";
	public int updateCount = 0;

    

    private float invincibleTime;
    private bool invincible = false;
    private Transform arm; //players'arm
    private Weapon myWeapon = null;
    private Transform weaponPoint; //where to put the weapon
    private Animator m_Anim;            // Reference to the player's animator component.
    private bool isSped = false;
    private bool isSlowed = false;

    private void Awake(){
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("isKill", false);
        turnInvincible(2);
    }

	void Update(){
        if (invincible && (Time.time-invinicbleDuration >invincibleTime)) {
            invincible = false;
        }
        if (transform.position.y <= fallBoundary)
        {
            damagePlayer(1000);
        }

        if (GameManager.gm)
        {
            if (true)
            {
                if (playerNum == 1)
                {
                    if (Input.GetButtonDown("Grab1"))
                    {//currently using the default fire 2 button, can change to some keys later
                        pickUp();
                    }
                    if (Input.GetButtonDown("Fire1"))
                    {
                        shoot();
                    }
                    else if (Input.GetButtonUp("Fire1"))
                    {
                        release();
                    }
                }
                else if (playerNum == 2)
                {
                    if (Input.GetButtonDown("Grab2"))
                    {//currently using the default fire 2 button, can change to some keys later
                        pickUp();
                    }
                    if (Input.GetButtonDown("Fire2"))
                    {
                        shoot();
                    }
                    else if (Input.GetButtonUp("Fire2"))
                    {
                        release();
                    }
					if(CharacterSelectionMenu.AION)
					{
						updateCount+=1;
						if(updateCount%8==0) Path = asterisk (transform.position.x,transform.position.y,2f,5f,0,false);
						string onEdge = edgeCheck(transform.position.x,transform.position.y,1f,5f);
						//if(onEdge!="NULL") print (onEdge);
						//print ("CHOSEN PATH : " + Path);
						UnityStandardAssets._2D.PlatformerCharacter2D player = transform.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
						//player.Move(0.0f,false,false);
						foreach(string term in Path.Split('-')){
							if(term!="")
							{
								if(term.Contains("LEFT"))
								{
									if(term=="MOVE LEFT ") AIshoot(Vector3.left);//SHOOT LEFT
									if((onEdge[0]=='O'&&onEdge[2]=='L')
									   ||term.Contains("JUMP"))player.Move (0f,false,true);//player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 400f));
									transform.position = new Vector3(transform.position.x-0.15f,transform.position.y,transform.position.z);
									//print ("LTERM + " + term);
								}
								else if(term.Contains("RIGHT"))
								{
									if(term=="MOVE RIGHT ") AIshoot(Vector3.right);//SHOOT RIGHT
									if((onEdge[0]=='O'&&onEdge[2]=='R')
									   ||term.Contains("JUMP"))player.Move (0f,false,true);//player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 400f));
									transform.position = new Vector3(transform.position.x+0.15f,transform.position.y,transform.position.z);
									//print ("RTERM + " + term);
								}
								break;
							}
						}
						
						
						//System specced to recognize Player2 as the A.I, also given this line that appears later:
						//&& hit.collider.name!="Thornton"
					}
				}
			}
		}
	}
	
	//[SerializeField] private LayerMask m_WhatIsGround;
	string edgeCheck(float x,float y,float startDist,float rayLength)
	{
		/*bool m_Grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().transform.Find ("GroundCheck").position, .2f, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				//Debug.Log("grounded");
				m_Grounded = true;
			}
			
		}
		if (!m_Grounded)
			return "NULL";*/
		RaycastHit2D rhit = Physics2D.Raycast (new Vector2 (0, 0), new Vector2 (0, 0));
		RaycastHit2D lhit = Physics2D.Raycast (new Vector2 (0, 0), new Vector2 (0, 0));
		Vector2 rdPos = new Vector2 (x + startDist / Mathf.Sqrt (2.0f), y - startDist / Mathf.Sqrt (2.0f));
		rhit = Physics2D.Raycast (rdPos, new Vector2 (1, -1), rayLength);
		Vector2 ldPos = new Vector2 (x - startDist / Mathf.Sqrt (2.0f), y - startDist / Mathf.Sqrt (2.0f));
		lhit = Physics2D.Raycast (ldPos, new Vector2 (-1, -1), rayLength);
		float a = lhit.point.y;
		float b = rhit.point.y;
		if (lhit != null && lhit.collider != null) if(rhit==null||rhit.collider==null) return ("O R ON EDGE AT RIGHT");
		if (rhit != null && rhit.collider != null) if(lhit==null||lhit.collider==null) return ("O L ON EDGE AT LEFT");
		if (lhit != null && rhit != null && lhit.collider != null && rhit.collider != null) {
			if (lhit.collider.tag == "Platform" && rhit.collider.tag == "Platform") {
				if (a != b) {
					if (a < b)
						return ("O L ON EDGE AT LEFT" + lhit.collider.tag + rhit.collider.tag);
					else
						return ("O R ON EDGE AT RIGHT" + lhit.collider.tag + rhit.collider.tag);
				}
			}
		}
		return "NULL";
	}
	
	string asterisk(float x, float y, float startDist, float rayLength, int depth=0, bool visible=false, string Orig="")
	{
		string retstr = "";
		string U = rayOfSight (x,y,startDist, rayLength, Vector2.up,depth,visible,Orig);
		string D = rayOfSight (x,y,startDist+0.5f, rayLength, Vector2.down,depth,visible,Orig);
		string L = rayOfSight (x,y,startDist, 10, Vector2.left,depth,visible,Orig);
		string R = rayOfSight (x,y,startDist, 10, Vector2.right,depth,visible,Orig);
		string RGR = rayOfSight (x,y-2,startDist, rayLength, Vector2.right,depth,visible,Orig);
		string LGR = rayOfSight (x,y-2,startDist, rayLength, Vector2.left,depth,visible,Orig);
		string UR = rayOfSight (x,y,startDist, rayLength, new Vector2(1,1),depth,visible,Orig);
		string UL = rayOfSight (x,y,startDist, rayLength, new Vector2(-1,1),depth,visible,Orig);
		string DR = rayOfSight (x,y,startDist, rayLength, new Vector2(1,-1),depth,visible,Orig);
		string DL = rayOfSight (x,y,startDist, rayLength, new Vector2(-1,-1),depth,visible,Orig);
		string URR = rayOfSight (x,y,startDist, rayLength, new Vector2(2,1),depth,visible,Orig);
		string UUR = rayOfSight (x,y,startDist, rayLength, new Vector2(1,2),depth,visible,Orig);
		string DRR = rayOfSight (x,y,startDist, rayLength, new Vector2(2,-1),depth,visible,Orig);
		string UUL = rayOfSight (x,y,startDist, rayLength, new Vector2(-1,2),depth,visible,Orig);
		string ULL = rayOfSight (x,y,startDist, rayLength, new Vector2(-2,1),depth,visible,Orig);
		string DLL = rayOfSight (x,y,startDist, rayLength, new Vector2(-2,-1),depth,visible,Orig);
		if (U!="NULL") retstr += Orig+(" " + U + "; ");
		if (D!="NULL") retstr += Orig+(" " + D + "; ");
		if (L!="NULL") retstr += Orig+(" " + L + "; ");
		if (R!="NULL") retstr += Orig+(" " + R + "; ");
		if (LGR!="NULL") retstr += Orig+(" " + LGR + "; ");
		if (RGR!="NULL") retstr += Orig+(" " + RGR + "; ");
		if (UR!="NULL") retstr += Orig+(" " + UR + "; ");
		if (UL!="NULL") retstr += Orig+(" " + UL + "; ");
		if (DR!="NULL") retstr += Orig+(" " + DR + "; ");
		if (DL!="NULL") retstr += Orig+(" " + DL + "; ");
		if (URR!="NULL") retstr += Orig+(" " + URR + "; ");
		if (UUR!="NULL") retstr += Orig+(" " + UUR + "; ");
		if (DRR!="NULL") retstr += Orig+(" " + DRR + "; ");
		if (UUL!="NULL") retstr += Orig+(" " + UUL + "; ");
		if (ULL!="NULL") retstr += Orig+(" " + ULL + "; ");
		if (DLL!="NULL") retstr += Orig+(" " + DLL + "; ");
		
		string[] paths = retstr.Split (';');
		ArrayList PathList = new ArrayList ();
		PathList.AddRange (paths);
		stringComparer ab = new stringComparer ();
		PathList.Sort(ab);
		string PathToTraverse = "";
		foreach (string path in PathList) {
			//print (path);
			if (path.Trim (' ') != "") {
				string PATH = "";
				//print ("POTENTIAL PATH: " + path);
				string[] terms = path.Split (' ');
				foreach (string term in terms) {
					if (term.Trim (' ') != "") {
						if(term.Length>2&&term[2]!='-'&&term[1]!='-')
						{
							if (term[0]=='U'&&term[1]=='U'&&term[2]=='L') PATH+= "HIGH JUMP LEFT -- ";
							else if (term[0]=='U'&&term[1]=='U'&&term[2]=='R') PATH+= "HIGH JUMP RIGHT -- ";
							else if (term[0]=='U'&&term[1]=='L'&&term[2]=='L') PATH+= "LONG JUMP LEFT -- ";
							else if (term[0]=='U'&&term[1]=='R'&&term[2]=='R') PATH+= "LONG JUMP RIGHT -- ";
							else if (term[0]=='D'&&term[1]=='L'&&term[2]=='L') PATH+= "SLIGHTLY FALL LEFT -- ";
							else if (term[0]=='D'&&term[1]=='R'&&term[2]=='R') PATH+= "SLIGHTLY FALL RIGHT -- ";
						}
						else if(term.Length>1&&term[1]!='-')
						{
							if (term[0]=='U'&&term[1]=='L') PATH+= "JUMP LEFT -- ";
							else if (term[0]=='U'&&term[1]=='R') PATH+= "JUMP RIGHT -- ";
							else if (term[0]=='D'&&term[1]=='L') PATH+= "FALL LEFT -- ";
							else if (term[0]=='D'&&term[1]=='R') PATH+= "FALL RIGHT -- ";
						}
						else
						{
							//if (term[0]=='D') PATH+= "MOVE DOWN -- ";
							//else if (term[0]=='U') PATH+= "JUMP -- ";
							if (term[0]=='L') PATH+= "MOVE LEFT -- ";
							else if (term[0]=='R') PATH+= "MOVE RIGHT -- ";
						}
						
						if(term.Length>6){
							if(term[term.Length-1]=='T'&&term[term.Length-2]=='A'&&term[term.Length-3]=='L'&&term[term.Length-4]=='P'){
								if(term[term.Length-5]=='R'){
									PATH+= ("PLATFORM CUTOFF AT RIGHT -- ");
								} else if(term[term.Length-5]=='L'){
									PATH+= ("PLATFORM CUTOFF AT LEFT -- ");
								}
							}
							else PATH+= (term+" ");
						}
						else PATH+= (term+" ");
					}
				}
				//print ("PATH REALIZED: " + PATH);
				//if(depth==0) print ("EMBRYONIC PATH: " + path);
				PathToTraverse = PATH;
				break;
			}
		}
		//print ("CHOSEN PATH: " + PathToTraverse);
		
		if (depth == 0)
			retstr = PathToTraverse;
		//print (retstr);
		return retstr;
	}
	
	public class stringComparer : IComparer
	{
		public int Compare(object xx,object yy)
		{
			string x = xx.ToString();
			string y = yy.ToString();
			if (x == null) {
				if (y == null)
					return 0;
				else
					return -1;
			} else {
				if (y == null)
					return 1;
				else {
					int retval = x.Length.CompareTo (y.Length);
					if (retval != 0)
						return retval;
					else
						return x.CompareTo (y);
				}
			}
		}
	}
	
	string rayOfSight(float x, float y, float startDist, float rayLength, Vector2 dir,int depth=0, bool visible=false, string Orig="")
	{
		float Units = rayLength;
		float DistFromPlayer = startDist;
		RaycastHit2D hit = Physics2D.Raycast (new Vector2 (0, 0), new Vector2 (0, 0));
		if (dir == Vector2.right) {
			Vector2 dPos = new Vector2 (x + DistFromPlayer, y);
			hit = Physics2D.Raycast (dPos, Vector2.right, Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + Vector3.right * DistFromPlayer;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == Vector2.up) {
			Vector2 dPos = new Vector2 (x, y + DistFromPlayer);
			hit = Physics2D.Raycast (dPos, Vector2.up, Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + Vector3.up * DistFromPlayer;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 90);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == Vector2.left) {
			Vector2 dPos = new Vector2 (x - DistFromPlayer, y);
			hit = Physics2D.Raycast (dPos, Vector2.left, Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + Vector3.left * DistFromPlayer;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 180);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == Vector2.down) {
			Vector2 dPos = new Vector2 (x, y - DistFromPlayer);
			hit = Physics2D.Raycast (dPos, Vector2.down, Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + Vector3.down * DistFromPlayer;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 270);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (1, 1)) {
			//UR
			Vector2 dPos = new Vector2 (x + DistFromPlayer / Mathf.Sqrt (2.0f), y + DistFromPlayer / Mathf.Sqrt (2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (1, 1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + new Vector3 (1, 1, 0) * DistFromPlayer / Mathf.Sqrt (2.0f);
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 45);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (-1, 1)) {
			//UL
			Vector2 dPos = new Vector2 (x - DistFromPlayer / Mathf.Sqrt (2.0f), y + DistFromPlayer / Mathf.Sqrt (2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (-1, 1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + new Vector3 (-1, 1, 0) * DistFromPlayer / Mathf.Sqrt (2.0f);
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 135);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (1, -1)) {
			//DR
			Vector2 dPos = new Vector2 (x + DistFromPlayer * Mathf.Sqrt (2.0f), y - DistFromPlayer * Mathf.Sqrt (2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (1, -1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + new Vector3 (1, -1, 0) * DistFromPlayer * Mathf.Sqrt (2.0f);
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 315);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (-1, -1)) {
			//DL
			Vector2 dPos = new Vector2 (x - DistFromPlayer * Mathf.Sqrt (2.0f), y - DistFromPlayer * Mathf.Sqrt (2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (-1, -1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = new Vector3 (x, y) + new Vector3 (-1, -1, 0) * DistFromPlayer * Mathf.Sqrt (2.0f);
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 225);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (2, 1)) {
			//URR
			Vector2 dPos = new Vector3 (x, y) + new Vector3 (2, 1, 0) * DistFromPlayer / Mathf.Sqrt (Mathf.Pow (2.0f, 2.0f) + Mathf.Pow (1.0f, 2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (2, 1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = dPos;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 22.5f);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (1, 2)) {
			//UUR
			Vector2 dPos = new Vector3 (x, y) + new Vector3 (1, 2, 0) * DistFromPlayer / Mathf.Sqrt (Mathf.Pow (1.0f, 2.0f) + Mathf.Pow (2.0f, 2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (1, 2), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = dPos;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 67.5f);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (-1, 2)) {
			//UUL
			Vector2 dPos = new Vector3 (x, y) + new Vector3 (-1, 2, 0) * DistFromPlayer / Mathf.Sqrt (Mathf.Pow (1.0f, 2.0f) + Mathf.Pow (2.0f, 2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (-1, 2), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = dPos;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 112.5f);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (-2, 1)) {
			//ULL
			Vector2 dPos = new Vector3 (x, y) + new Vector3 (-2, 1, 0) * DistFromPlayer / Mathf.Sqrt (Mathf.Pow (1.0f, 2.0f) + Mathf.Pow (2.0f, 2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (-2, 1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = dPos;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 157.5f);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		} else if (dir == new Vector2 (2, -1)) {
			//DRR
			Vector2 dPos = new Vector3 (x, y) + new Vector3 (2, -1, 0) * DistFromPlayer / Mathf.Sqrt (Mathf.Pow (2.0f, 2.0f) + Mathf.Pow (1.0f, 2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (2, -1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = dPos;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 337.5f);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
			/*}  else if (dir == new Vector2 (1, -2)) {
			//DDR
			Vector2 dPos = transform.position + new Vector3(1,-2,0) * DistFromPlayer / Mathf.Sqrt(Mathf.Pow(1.0f,2.0f)+Mathf.Pow(2.0f,2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2(1,-2), Units);
			lineOfSight.transform.position = dPos;
			lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
			lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 292.5f);
		}   else if (dir == new Vector2 (-1, -2)) {
			//DDL
			Vector2 dPos = transform.position + new Vector3(-1,-2,0) * DistFromPlayer / Mathf.Sqrt(Mathf.Pow(1.0f,2.0f)+Mathf.Pow(2.0f,2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2(-1,-2), Units);
			lineOfSight.transform.position = dPos;
			lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
			lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 247.5f);
		*/
		} else if (dir == new Vector2 (-2, -1)) {
			//DLL
			Vector2 dPos = new Vector3 (x, y) + new Vector3 (-2, -1, 0) * DistFromPlayer / Mathf.Sqrt (Mathf.Pow (1.0f, 2.0f) + Mathf.Pow (2.0f, 2.0f));
			hit = Physics2D.Raycast (dPos, new Vector2 (-2, -1), Units);
			if (visible) {
				GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				lineOfSight.transform.position = dPos;
				lineOfSight.transform.localScale = new Vector3 (Units, 1, 1);
				lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 202.5f);
				StartCoroutine (Wait (0.25f, lineOfSight));
			}
		}
		string DIR = "";
		if (hit != null && hit.collider != null && this.GetComponent<Collider2D>() != hit.collider) {
			switch (dir.ToString ()) {
			case "(1.0, 0.0)":
				DIR = "R";
				break;
			case "(-1.0, 0.0)":
				DIR = "L";
				break;
			case "(0.0, 1.0)":
				DIR = "U";
				break;
			case "(0.0, -1.0)":
				DIR = "D";
				break;
			case "(1.0, 1.0)":
				DIR = "UR";
				break;
			case "(1.0, -1.0)":
				DIR = "DR";
				break;
			case "(-1.0, 1.0)":
				DIR = "UL";
				break;
			case "(-1.0, -1.0)":
				DIR = "DL";
				break;
			case "(2.0, 1.0)":
				DIR = "URR";
				break;
			case "(-2.0, 1.0)":
				DIR = "ULL";
				break;
			case "(2.0, -1.0)":
				DIR = "DRR";
				break;
			case "(-2.0, -1.0)":
				DIR = "DLL";
				break;
			case "(1.0, 2.0)":
				DIR = "UUR";
				break;
			case "(-1.0, 2.0)":
				DIR = "UUL";
				break;
			default:
				DIR = dir.ToString();
				break;
			}
			if(hit.collider.tag=="Platform")
			{
				//rayOfSight(hit.collider.transform.position.x,hit.collider.transform.position.y,1f,3f,Vector2.up);
				//asterisk(hit.collider.transform.position.x,hit.collider.transform.position.y,0f,3f);
				/*GameObject LOS = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				LOS.transform.position = new Vector3(hit.collider.transform.position.x-hit.collider.bounds.size.x/2-hit.collider.offset.x,
				                                     hit.collider.transform.position.y+hit.collider.bounds.size.y/2-hit.collider.offset.y);
				LOS.transform.localScale = new Vector3 (5, 1, 1);
				LOS.transform.localRotation = Quaternion.Euler (1, 1, 90);
				StartCoroutine(Wait(0.25f,LOS));
				GameObject LOS2 = (GameObject)Instantiate (Resources.Load ("UnitTrail"));
				LOS2.transform.position = new Vector3(hit.collider.transform.position.x+hit.collider.bounds.size.x/2-hit.collider.offset.x,
				                                      hit.collider.transform.position.y+hit.collider.bounds.size.y/2-hit.collider.offset.y);
				LOS2.transform.localScale = new Vector3 (5, 1, 1);
				LOS2.transform.localRotation = Quaternion.Euler (1, 1, 90);
				StartCoroutine(Wait(0.25f,LOS2));*/
				if(depth<1)
				{
					return /*asterisk(hit.collider.transform.position.x//+hit.collider.bounds.size.x/2-hit.collider.offset.x,
					                	+hit.collider.bounds.size.x+1,
					                hit.collider.transform.position.y//+hit.collider.bounds.size.y/2-hit.collider.offset.y+1,
					                	+hit.collider.bounds.size.y,
					                1.5f,3.5f,depth+1,visible,Orig+" "+DIR+"-RPLAT") + " ; " +
						asterisk(hit.collider.transform.position.x-1//-hit.collider.bounds.size.x/2-hit.collider.offset.x,
						         ,//+hit.collider.bounds.size.x,
						         hit.collider.transform.position.y//+hit.collider.bounds.size.y/2-hit.collider.offset.y+1,
						         +hit.collider.bounds.size.y,
						         1.5f,3.5f,depth+1,visible,Orig+" "+DIR+"-LPLAT") + " ; " +*/
					asterisk(hit.collider.transform.position.x//+hit.collider.bounds.size.x/2-hit.collider.offset.x,
					         +hit.collider.bounds.size.x+1,
					         hit.collider.transform.position.y//+hit.collider.bounds.size.y/2-hit.collider.offset.y+1,
					         +hit.collider.bounds.size.y+1,
					         startDist,rayLength,depth+1,visible,Orig+" "+DIR+"-RPLAT") + " ; " +
						asterisk(hit.collider.transform.position.x-1//-hit.collider.bounds.size.x/2-hit.collider.offset.x,
						         ,//+hit.collider.bounds.size.x,
						         hit.collider.transform.position.y//+hit.collider.bounds.size.y/2-hit.collider.offset.y+1,
						         +hit.collider.bounds.size.y+1,
						         startDist,rayLength,depth+1,visible,Orig+" "+DIR+"-LPLAT");
				}
				//return ("HIT PLATFORM AT " + hit.point + " FROM " + DIR);
			}
			//else if(hit.collider.tag=="Item") return (Orig + " " + DIR + " HIT ITEM AT " + hit.point);
			else if(hit.collider.tag=="Player" && hit.collider.name!="Thornton(Clone)" && hit.collider.name!="Ground 1" && hit.collider.name!="Ground 2") return (Orig + " " + DIR + " HIT PLAYER AT " + hit.point + " " + hit.collider.name);
			//else return (Orig + " " + DIR + " HIT " + hit.collider.tag + " AT " + hit.point);
		}
		return "NULL";
	}
	
	public void AIshoot(Vector3 dir) {
		GameObject lineOfSight = (GameObject)Instantiate (Resources.Load ("daggerTrail"));
		lineOfSight.GetComponent<Damager> ().setDamage(100);
		lineOfSight.transform.position = new Vector3(transform.position.x+1f,transform.position.y,transform.position.z);
		if (dir == Vector3.left) {
			lineOfSight.transform.position = new Vector3(transform.position.x-1f,transform.position.y,transform.position.z);
			lineOfSight.transform.localRotation = Quaternion.Euler (1, 1, 180);
		}
		StartCoroutine (Wait (20f, lineOfSight));
	}
	
	IEnumerator Wait(float time,GameObject lineOfSight)
	{
		yield return new WaitForSeconds (time);
		Destroy (lineOfSight);
	}

   void OnTriggerEnter2D(Collider2D other)
    {
 
        if(other.gameObject.tag == "Spikes")
        {
            Debug.Log("spikes");
            damagePlayer(1000);
        }
        if (other.gameObject.tag == "teleporter") {

            transform.position = other.gameObject.GetComponent<teleporter>().destination(transform.position);
        }
    }

	public void damagePlayer(int damage){
        if (!invincible) {
            playerStats.Health -= damage;
            if (playerStats.Health <= 0)
            {
                //Debug.Log("Player Is Kill");
                //m_Anim.SetBool("isKill", true);
                GameManager.killPlayer(this);
            }
        }
	}

    public void setPlayerNum(int playerNumber){
        this.playerNum = playerNumber;
        Debug.Log("player number is now: " + playerNumber);
    }

    public void release() {
        if (myWeapon != null) {
            myWeapon.Release();
        }
    }

    public void shoot() {
        if (myWeapon != null) {
            myWeapon.Shoot();
        }
    }

    public void turnInvincible(int duration) {
        invincible = true;
        invinicbleDuration = duration;
        invincibleTime = Time.time;
    }

    public void setIsSlowed(bool slowed)
    {
        isSlowed = slowed;
    }

    public void setIsSped(bool sped)
    {
        isSped = sped;
    }

    public bool getIsSlowed()
    {
        return isSlowed;
    }

    public bool getIsSped()
    {
        return isSped;
    }

    public Weapon getWeapon() {
        return myWeapon;
    }
    public void pickUp() {
        arm = transform.FindChild("arm");
        weaponPoint = arm.FindChild("weaponPoint");
        Object[] o = FindObjectsOfType(typeof(Weapon));//look for all weapons on the map
        foreach (Weapon n in o) {
            if (Physics2D.IsTouching(this.GetComponent<BoxCollider2D>(), 
                        ((Weapon)n).GetComponent<Collider2D>())) { //check is the player touching any weapon
                if (arm.childCount>1) {// Destroy the old weapon in arm
                    Debug.Log("touching");
                    Destroy(arm.GetChild(1).gameObject);
                    myWeapon = null;

                }
                n.transform.SetParent(arm);  // attach new weapon to arm
                Vector2 weaponPoistion = new Vector2(weaponPoint.position.x, weaponPoint.position.y);
                n.transform.position = weaponPoistion;
                n.transform.rotation = arm.rotation;
                myWeapon = (Weapon)n;
                break;
            }
        }

    }




}
