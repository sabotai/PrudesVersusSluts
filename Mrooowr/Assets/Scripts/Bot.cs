using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour {
	public bool swap;
	public bool attack;
	GameObject[] chars;
	public float distThresh = 2f;
	public float hori = 0f;
	public float vert = 0f;
	public GameObject target;
	//public GameObject current;

	// Use this for initialization
	void Start () {
		UpdateEmenies();
		swap = false;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateEmenies();
		hori = 0f;
		vert = 0f;

		if (target != null) {
			Attak();
		}


	}

	void UpdateEmenies(){
		chars = GameObject.FindGameObjectsWithTag("Characters");


		//for (int i = 0; i < transform.childCount; i++){
			//if (GetComponent<Hurt>().doHurt && Time.frameCount % 120 == 0) swap = true;


			if (GetComponent<Move>().selected){
				//current = soldier;
				//update every once in a while
				if (Time.frameCount % 60 == 0) {
					GameObject rando = chars[Random.Range(0,chars.Length)];
					//foreach(GameObject char_ in chars){
						if (rando.GetComponent<Move>() && rando.transform.parent != transform.parent){ //is emeny?
							target = rando;
						}
					//}
				}


			}
		//}

	}

	void Attak(){
		//Debug.Log("found uh emeny");
		float dist = Vector3.Distance(target.transform.position, transform.position);
		if (dist < distThresh){ //close
			Debug.Log("ATTTTTAAACK");
			if (Time.frameCount % 60 == 0) attack = true;
			UpdateEmenies();
			//if (Time.frameCount % 180 == 0) swap = true;
		} else { //farther away
			if (dist < distThresh * 10f) {
				if (target.transform.position.x < transform.position.x) {
					hori = -1f;
				} else {
					hori = 1f;
				}

				if (target.transform.position.y < transform.position.y) {
					vert = -1f;
				} else {
					vert = 1f;
				}

			}
		} 
	}
}