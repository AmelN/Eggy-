using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformRotation : MonoBehaviour {

	private bool rotateRight, rotateLeft;
	private int angle;
	// Use this for initialization
	void Start () {
		rotateRight = true;
		rotateLeft = true;

	}
	
	// Update is called once per frame
	void Update () {
		AngleCase ();
		//this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, angle);	
		this.transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	void AngleCase (){
		if (angle == 0 && Input.GetButtonDown ("RotateLeft") || Input.GetAxis ("RotateRigh") != 0) {
			angle = 90;
		} else if (angle == 90 && Input.GetButtonDown ("RotateRight") || Input.GetAxis ("RotateLef") != 0) {
			angle = 0;
		} else if (angle == 0 && Input.GetButtonDown ("RotateRight") || Input.GetAxis ("RotateLef") != 0) {
			angle = -90;
		} else if (angle == -90 && Input.GetButtonDown ("RotateLeft") || Input.GetAxis ("RotateRigh") != 0) {
			angle = 0;
		}
	}
}

