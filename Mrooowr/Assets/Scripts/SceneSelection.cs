using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelection : MonoBehaviour {

	int selection = 0;
	public GameObject prudes, sluts;
	public Text subAnnouncer;
	public float textDelay = 2f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > textDelay){
			subAnnouncer.transform.parent.gameObject.SetActive(true);
			subAnnouncer.text = "Choose your battleground";
		}
		if (Input.anyKeyDown){
			if (Input.GetButtonDown("P1_Action") || Input.GetButtonDown("P2_Action")){
				prudes.SetActive(true);
				sluts.SetActive(true);
				this.enabled = false;
			}

			if (Input.GetAxis("P1_Horizontal") > 0f || Input.GetAxis("P2_Horizontal") > 0f ){
				if (selection < transform.childCount - 1){
					selection++;
				} else {
					selection = 0;
				}
			}
			if (Input.GetAxis("P1_Horizontal") < 0f || Input.GetAxis("P2_Horizontal") < 0f ){
				if (selection > 0){
					selection--;
				} else {
					selection = transform.childCount - 1;
				}
			}

			for (int i = 0; i < transform.childCount; i++){
				transform.GetChild(i).gameObject.SetActive(false);
			}

			transform.GetChild(selection).gameObject.SetActive(true);
		}
	}
}
