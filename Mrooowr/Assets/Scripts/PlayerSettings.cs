using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {
	int selection = 1;
	public Bot bot;
	public Transform botHolder;
	public GameObject p1Controls, p2Controls, pruuds, sloots;
	public SceneSelection sceneS;
	public GameObject sceneSUI;
	public Text subAnnouncer;
	GameObject man;

	// Use this for initialization
	void Start () {
		man = Camera.main.gameObject;
		
		p1Controls.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (selection == 1) subAnnouncer.text = "" + selection + " > Player";
		if (selection == 2) subAnnouncer.text = "< " + selection + " Players";
		if (Input.anyKeyDown){
			if (Input.GetButtonDown("P1_Action") || Input.GetButtonDown("P2_Action")){
				//p1Controls.SetActive(true);
				Manager.numPlayers = selection;
				if (selection == 2) {

					sceneS.enabled = true;
					sceneSUI.SetActive(true);
				} else {
					pruuds.SetActive(true);
					for (int i = 0; i < sloots.transform.childCount; i++){
						Destroy(sloots.transform.GetChild(i).gameObject);//.gameObject.SetActive(false);
						//sloots.transform.DetachChildren();
					}
					for (int i = 0; i < botHolder.childCount; i++){
						botHolder.GetChild(0).parent = sloots.transform;
					}
					sloots.SetActive(true);
				}

				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().confirmClip, 0.85f);
				gameObject.SetActive(false);
				//GetComponent<Image>().enabled = false;
				this.enabled = false;

			}

			if (Input.GetAxis("P1_Horizontal") > 0f || Input.GetAxis("P2_Horizontal") > 0f ){
				selection = 2;
				//bot.enabled = false;
				p2Controls.SetActive(true);

				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);
			}
			if (Input.GetAxis("P1_Horizontal") < 0f || Input.GetAxis("P2_Horizontal") < 0f ){
				selection = 1;
				//bot.enabled = true;
				p2Controls.SetActive(false);

				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);

			}


			//transform.GetChild(selection).gameObject.SetActive(true);
		}
	}
}
