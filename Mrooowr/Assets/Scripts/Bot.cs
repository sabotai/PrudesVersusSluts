using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour {
	public bool swap;
	public bool attack;
	public Vector3 dir;
	GameObject[] chars;
	public float distThresh = 2f;
	public float hori = 0f;
	public float vert = 0f;

	// Use this for initialization
	void Start () {
		UpdateEmenies();
	}
	
	// Update is called once per frame
	void Update () {
		//update every once in a while
		if (Time.frameCount % 60 == 0) UpdateEmenies();
		hori = 0f;
		vert = 0f;

		for (int i = 0; i < transform.childCount; i++){
			GameObject current = transform.GetChild(i).gameObject;


			if (current.GetComponent<Move>().selected){
				foreach(GameObject char_ in chars){
					if (char_.transform.parent != transform){ //is emeny
						Debug.Log("found uh emeny");
						float dist = Vector3.Distance(char_.transform.position, current.transform.position);
						if (dist < distThresh){
							if (Time.frameCount % 10 == 0) attack = true;
						} else {
							if (dist < distThresh * 3f) {
								if (char_.transform.position.x < transform.position.x) hori = -1f;
								else hori = 1f;

								if (char_.transform.position.y < transform.position.y) vert = -1f;
								else vert = 1f;

							} else if (Time.frameCount % 120 == 0) {
								swap = true;
							}
						}

					}
				}


			}
		}
	}

	void UpdateEmenies(){
		chars = GameObject.FindGameObjectsWithTag("Characters");
	}
}
