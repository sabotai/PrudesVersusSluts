using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextTimeout : MonoBehaviour {

	float startTime;
	public float timerAmt = 3f;
	public bool timerRunning = true;
	public GameObject next;
	public bool allowSkip = false;
	public bool disableParent = true;
	public bool keyResetTimer = true;
	// Use this for initialization
	void Start () {
		//timerRunning = true;
		//startTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {

		if ((Time.timeSinceLevelLoad > startTime + timerAmt + Intro.introTime && timerRunning) || (Time.timeSinceLevelLoad > 0.5f && allowSkip && Input.anyKeyDown)){ //timer is up
			timerRunning = false;
			if (disableParent) GetComponent<Text>().text = "";
			if (next != null) {
				next.SetActive(true);
				next = null;
			}
			if (transform.parent.GetComponent<Image>() && disableParent) transform.parent.GetComponent<Image>().enabled = false;
			//this.enabled = false;
			
		} else if (GetComponent<Text>().text != "" && !timerRunning){ //new text
			if (transform.parent.GetComponent<Image>()) transform.parent.GetComponent<Image>().enabled = true;
			timerRunning = true;
			allowSkip = false;
			startTime = Time.timeSinceLevelLoad;
			//Debug.Log("introtime = " + Intro.introTime);
		}
		if (next == null) allowSkip = false; //no skipping the endgame?
		//if (Manager.gameOver) this.enabled = false;
	}
}
