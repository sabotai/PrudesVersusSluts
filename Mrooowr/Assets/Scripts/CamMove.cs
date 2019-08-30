using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

	int charsOnScreen = 0;
	public Transform slutParent, prudeParent;
	public bool max = false;
	float maxRight, maxLeft;
	public float speed = 0.6f;
	GameObject[] reversed;
	GameObject[] background;
	public float parallaxAmt = 0.5f;

	// Use this for initialization
	void Start () {	
		Manager.howManyChars = slutParent.childCount + prudeParent.childCount;
		maxRight = GameObject.Find("RightBound").transform.position.x;
		maxLeft = GameObject.Find("LeftBound").transform.position.x;
		reversed = GameObject.FindGameObjectsWithTag("Foreground");
		background = GameObject.FindGameObjectsWithTag("Background");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = Vector3.Lerp(transform.position, AvgCharPos(), speed);
		if (reversed.Length > 0) {
			foreach(GameObject fore in reversed)
				fore.transform.position += parallaxAmt * (transform.parent.position - move);
		}

		if (background.Length > 0) {
			foreach(GameObject back in background)
				back.transform.position += -0.5f * parallaxAmt * (transform.parent.position - move);
		}
		transform.parent.position = move;//Vector3.Lerp(transform.position, AvgCharPos(), speed);


	}
	Vector3 AvgCharPos(){
		Vector3 avg = Vector3.zero;
		for (int i = 0; i < prudeParent.childCount; i++){
			avg += prudeParent.GetChild(i).position;
		}

		if (!Manager.usingBots){
			for (int i = 0; i < slutParent.childCount; i++){
				avg += slutParent.GetChild(i).position;
			}
		} 
		
		if (!Manager.usingBots){
		avg /= (slutParent.childCount + prudeParent.childCount);
		} else {
			avg /= prudeParent.childCount;
		}
		avg = new Vector3(Mathf.Clamp(avg.x, maxLeft - transform.GetChild(0).localPosition.x, maxRight - transform.GetChild(1).localPosition.x), transform.position.y, transform.position.z);


		return avg;
	}
/*
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag("Characters")){
			charsOnScreen++;
		} else if (col.gameObject.CompareTag("Bounds")){
			max = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.CompareTag("Characters")){
			charsOnScreen--;
		} else if (col.gameObject.CompareTag("Bounds")){
			max = false;
		}
	}
	*/
}
