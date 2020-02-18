using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDisable : MonoBehaviour
{
	float startTime;
	public float timerAmt = 3f;
	public bool shrink = false;
    // Start is called before the first frame update
    void Start()
    {
       	startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + timerAmt) {
        	if (shrink && transform.localScale.x > 0.1f){
				transform.localScale *= 0.75f;

			} else {
        		gameObject.SetActive(false);
        	}
        }
    }
}
