using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextTimeout : MonoBehaviour {

	float startTime;
	public float timerAmt = 3f;
	public bool timerRunning = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > startTime + timerAmt && timerRunning){ //timer is up
			timerRunning = false;
			GetComponent<Text>().text = "";
			transform.parent.GetComponent<Image>().enabled = false;

			
		} else if (GetComponent<Text>().text != "" && !timerRunning){ //new text?
			transform.parent.GetComponent<Image>().enabled = true;
			timerRunning = true;
			startTime = Time.timeSinceLevelLoad;
		}

		if (Manager.gameOver) this.enabled = false;
	}
}
