using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds; // Array List of Backgrounds

	private float[] parallaxScales; //how much the background image moves
	public float smothing = 1; // how smooth the paralax will be

	private Transform cam; //reference to camera transform 
	private Vector3 previousCamPosition;// stores position of camera in previous frame

	//called before start(); great for references
	void Awake(){
		//set up camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		//the previevios fram has current frame position
		previousCamPosition = cam.position;

		//assinging corisponding paralaxScale
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;

		}
	}
	
	// Update is called once per frame
	void Update () {

		//for each background
		for (int i = 0; i < backgrounds.Length; i++) {
			//parallax is opposite of camera movment. previous fram mult by scale
			float paralax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

			//set target x position. current position plus paralax
			float backgroundTargetPosX = backgrounds[i].position.x + paralax;

			//create target position which is background current position with it's target position
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			//fade between current anf target position
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smothing * Time.deltaTime);
		}

		//set previous cam position to position at end of frame
		previousCamPosition = cam.position;
	
	}
}
