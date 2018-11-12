using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

	public int player = 1;
	int selected = 0;
	public bool begun = false;
	public Text announcer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("P" + player + "_Next") && begun){
			CycleSelection();
		}

		if (transform.childCount == 0 && !Manager.gameOver){
			if (player == 1){
				announcer.text = "Prudes Win!";
			} else {
				announcer.text = "Sluts Win!";
			}
			Manager.gameOver = true;
		}

	}
	public void CycleSelection(){

			if (selected < transform.childCount - 1) {
				selected++; 
			} else {
				selected = 0;
			}
			if (transform.childCount > 0){
				AudioSource aud = transform.GetChild(selected).gameObject.GetComponent<AudioSource>();
				aud.PlayOneShot(aud.clip);
			}
			Select();
	}
	public void Select(){
		if (!begun){
			Debug.Log("selecting " + transform.GetChild(1).gameObject.name);
			transform.GetChild(2).gameObject.GetComponent<Move>().selected = true;
			begun = true;
		}

		for(int i = 0; i < transform.childCount; i++){
			if (!transform.GetChild(0).gameObject.CompareTag("Slot") && !transform.GetChild(0).gameObject.CompareTag("Wall")){
				transform.GetChild(i).gameObject.GetComponent<Move>().selected = false;
			}
		}
		if (transform.childCount > 0 && transform.GetChild(selected).gameObject.CompareTag("Characters"))	transform.GetChild(selected).gameObject.GetComponent<Move>().selected = true;
	
	}
}
