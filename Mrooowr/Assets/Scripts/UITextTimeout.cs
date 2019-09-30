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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.timeSinceLevelLoad > startTime + timerAmt && timerRunning) || (Time.timeSinceLevelLoad > 0.5f && allowSkip && Input.anyKeyDown)){ //timer is up
			timerRunning = false;
			if (disableParent) GetComponent<Text>().text = "";
			if (next != null) {
				next.SetActive(true);
				next = null;
			}
			if (transform.parent.GetComponent<Image>() && disableParent) transform.parent.GetComponent<Image>().enabled = false;
			//this.enabled = false;
			
		} else if (GetComponent<Text>().text != "" && !timerRunning){ //new text?
			if (transform.parent.GetComponent<Image>()) transform.parent.GetComponent<Image>().enabled = true;
			timerRunning = true;
			allowSkip = false;
			startTime = Time.timeSinceLevelLoad;
		}
		if (next == null) allowSkip = false;
		//if (Manager.gameOver) this.enabled = false;
	}
}
