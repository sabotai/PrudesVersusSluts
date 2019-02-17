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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.timeSinceLevelLoad > startTime + timerAmt && timerRunning) || (allowSkip && (Input.GetButtonDown("P1_Action") || Input.GetButtonDown("P2_Action")))){ //timer is up
			timerRunning = false;
			GetComponent<Text>().text = "";
			if (next != null) next.SetActive(true);
			if (transform.parent.GetComponent<Image>()) transform.parent.GetComponent<Image>().enabled = false;
			this.enabled = false;
			
		} else if (GetComponent<Text>().text != "" && !timerRunning){ //new text?
			if (transform.parent.GetComponent<Image>()) transform.parent.GetComponent<Image>().enabled = true;
			timerRunning = true;
			startTime = Time.timeSinceLevelLoad;
		}

		if (Manager.gameOver) this.enabled = false;
	}
}
