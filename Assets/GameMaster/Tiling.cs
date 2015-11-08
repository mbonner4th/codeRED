using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2; //offset to avoid erros

	//checking if stuff is instanciated
	public bool hasRightBuddy = false;
	public bool hasLeftBuddy =false;

	public bool reverseScale = false; //used if the object is not tilable

	private float spriteWidth = 0f; // width of our element
	private Camera cam;
	private Transform myTransform;

	void Awake(){
		// sets references before script
		cam = Camera.main;
		myTransform = transform;
	}


	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	
	}
	
	// Update is called once per frame
	void Update () {
		//does it need budies? if not, do nothing
		if (hasLeftBuddy == false || hasRightBuddy == false) {
			//calculates extent of what camera can see (in cordinates)
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height; //////////////////////////////////////////

			//cal. x position where camera can see edge of sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x + spriteWidth/2) + camHorizontalExtend;
		
			//checking if we can see eddge of the element. Calls MakeNewBuddy if we can
			if (cam.transform.position.x >= (edgeVisiblePositionRight - offsetX) && hasRightBuddy == false){

				//Debug.Log("Right Hit");

				MakeNewBuddy(1);
				hasRightBuddy = true;
			}
			else if (cam.transform.position.x <= (edgeVisiblePositionLeft - offsetX) && hasLeftBuddy == false){

				//Debug.Log("Left hit");
				MakeNewBuddy(-1);
				hasLeftBuddy = true;

			}
		}

	
	}

	//function that creates a buddy on the side required
	void MakeNewBuddy (int rightOrLeft){
		//calculate new position for new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft,
		                                   myTransform.position.y, myTransform.position.z);
		//instantiating new buddy and storing in a varible 
		Transform newBuddy = (Transform)Instantiate (myTransform, newPosition, myTransform.rotation);

		//if not tilable, lets reverse the x size of our object to get rid of seems
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3(newBuddy.localScale.x*-1,
			                                  newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;

		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling> ().hasLeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tiling> ().hasRightBuddy = true;
		}

	}

}
