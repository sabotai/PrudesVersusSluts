using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

	public int player = 1;
	int selected = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("P" + player + "_Next")){
			CycleSelection();
		}

	}
	public void CycleSelection(){

			if (selected < transform.childCount - 1) {
				selected++; 
			} else {
				selected = 0;
			}
			Select();
	}
	public void Select(){


			for(int i = 0; i < transform.childCount; i++){
				transform.GetChild(i).gameObject.GetComponent<Move>().selected = false;
			}
			if (transform.childCount > 0)	transform.GetChild(selected).gameObject.GetComponent<Move>().selected = true;

	}
}
