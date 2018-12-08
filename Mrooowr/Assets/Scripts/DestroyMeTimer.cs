using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeTimer : MonoBehaviour {
	float startTime;
	public float timerAmt = 3f;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + timerAmt) Destroy(gameObject);
	}
}
