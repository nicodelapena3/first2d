using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;    //Array for backgrounds to be parallaxed
	private float[] parallaxScales;    //Proportion of camera's movement to move backgrounds
	public float smoothing = 1f;       //How smooth the parallax effect will be. Must be above 0!

	private Transform cam;             //reference to main camera
	private Vector3 previousCamPos;    //the position of the camera in the previous frame

	//	Is called before Start(). Good for references
	void Awake () {
		//set up camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// the previous frame had the current frame's camera position
		previousCamPos = cam.position;

		// assigning corresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds [i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		// for each background
		for (int i = 0; i < backgrounds.Length; i++) {
			// he parallax is the opposite of the camera movement because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds [i].position.x + parallax;

			//create a target pos which is the background's current position with its target x pos
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		//set the previousCamPos to the camera's position at the end of the frame
		previousCamPos = cam.position;
	}
}
