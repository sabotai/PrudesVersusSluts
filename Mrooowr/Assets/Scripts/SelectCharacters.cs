using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;
using UnityEngine.UI;

public class SelectCharacters : MonoBehaviour {

	public GameObject[] prefab;
	int currentSelection = 0;
	int player;
	public Text sub;
	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject.GetComponent<Selection>().player;
		sub.text = "Choose your sluts/prudes!";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown){ //limit to only when key is being pressed
			if (Input.GetAxis("P" + player + "_Horizontal") > 0f){
				if (currentSelection < prefab.Length - 1){
					currentSelection++;
				} else {
					currentSelection = 0;
				}
				GetComponent<DoodleAnimator>().File = prefab[currentSelection].GetComponent<DoodleAnimator>().File;
				transform.localScale = prefab[currentSelection].transform.localScale;
			}
			
			if (Input.GetAxis("P" + player + "_Horizontal") < 0f){
				if (currentSelection > 0){
					currentSelection--;
				} else {
					currentSelection = prefab.Length - 1;
				}
				GetComponent<DoodleAnimator>().File = prefab[currentSelection].GetComponent<DoodleAnimator>().File;
				transform.localScale = prefab[currentSelection].transform.localScale;
			}
		}

		if (Input.GetButtonDown("P" + player + "_Action")){
			Quaternion rot;
			if (player == 1){
				rot = Quaternion.identity;
			} else {
				rot = Quaternion.Euler(0f, 180f, 0f);
			}
			GameObject newbie = Instantiate(prefab[currentSelection], transform.position, rot, transform.parent);
			if (player == 1){
				newbie.GetComponent<Move>().facingRight = true;
				} else {
				newbie.GetComponent<Move>().facingRight = false;

				}
			newbie.name = prefab[currentSelection].name;


			GameObject sib = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject;
			if (sib.CompareTag("Slot")){
				//then activate the next one
				sib.SetActive(true);
			} else {
				//begin game for me
				if (player == 1) Manager.p1Ready = true; else if (player == 2) Manager.p2Ready = true;
				if (sib.CompareTag("Wall")) Destroy(sib.gameObject);
				sub.text = "";
				sub.transform.parent.gameObject.SetActive(false);
				transform.parent.gameObject.GetComponent<Selection>().Select();
			}
			Destroy(gameObject);//.SetActive(false);
		}
	
		
	}
}
