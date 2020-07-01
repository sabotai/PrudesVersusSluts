﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	float startTime;
	public float timerAmt = 3f;
	public bool timerRunning = true;
	public GameObject next;
	public bool allowSkip = false;
	public static float introTime = 0f;
	// Use this for initialization
	void Start () {
		
		next.SetActive(false);
		introTime = 0f;

 	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Time.timeSinceLevelLoad > startTime + timerAmt && timerRunning) || (Time.time > 1.5f && allowSkip && (Input.anyKeyDown))){ //timer is up
			timerRunning = false;
			introTime = Time.timeSinceLevelLoad;
			if (next != null) {
				next.SetActive(true);
				next = null;
				gameObject.SetActive(false);
			}
			
		}
	}
}
