using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public bool recording = true;

	private bool isPaused = false;
	private float initialFixedDeltaTime;

	void Start () {
		initialFixedDeltaTime = Time.fixedDeltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButton("Fire1")) { 
			recording = false;
		} else {
			recording = true;
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			if (!isPaused) {
				PauseGame();
			} else {
				ResumeGame ();
			}
		}

		//print ("Update");
	}

	void PauseGame () {
		Time.timeScale = 0;
		Time.fixedDeltaTime = 0;
		isPaused = true;
	}

	void ResumeGame () {
		Time.timeScale = 1;
		Time.fixedDeltaTime = initialFixedDeltaTime;
		isPaused = false;
	}
}
