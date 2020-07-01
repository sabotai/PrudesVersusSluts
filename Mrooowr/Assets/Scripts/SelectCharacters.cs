using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;
using UnityEngine.UI;

public class SelectCharacters : MonoBehaviour {

	public GameObject[] prefab;
	public int currentSelection = 0;
	int player;
	public Text sub;
	public AudioClip selectSound;
	AudioSource aud;
	public GameObject uiInstructions, instructions;
	Bot bot;
	public 

    float debounce = 0f;
    public float repeat = 0.2f;  // reduce to speed up auto-repeat input
    bool axisReady = true;

    // Use this for initialization
    void Start () {
		player = transform.parent.gameObject.GetComponent<Selection>().player;
		sub.text = "Choose your three characters!";// + Randomizer.prudeName + "(P1) / " + Randomizer.slutName + "(P2)!";
		aud = Camera.main.GetComponent<AudioSource>();
		//GetComponent<DoodleAnimator>().ChangeAnimation(prefab[currentSelection].GetComponent<DoodleAnimator>().File;
		transform.localScale = prefab[currentSelection].transform.localScale;
		bot = GetComponent<Bot>();
	}
	
	// Update is called once per frame
	void Update ()
    {
    	if (Manager.numPlayers == 2 || transform.parent.GetComponent<Selection>().player == 2){
	    	// OLD combined method
	        float vAxis = Input.GetAxisRaw("P" + player + "_Vertical");
	        float hAxis = Input.GetAxisRaw("P" + player + "_Horizontal");                                                      //bot
	        float now = Time.realtimeSinceStartup;
	        // check if user let go of the stick; if so, reset the input bounce control
	        if (Mathf.Abs(hAxis) < 0.1f)
	        {
	            debounce = 0f;
	        }

	        // if it's been long enough since the last input, then we allow it
	        if (now - debounce > repeat || debounce == 0f)
	        {
	            axisReady = true;

	            //debounce = Time.realtimeSinceStartup;
	        }
	        else
	        {
	            axisReady = false;
	        }
	        //Debug.Log("vAxis = " + vAxis + " // hAxis = " + hAxis + " // axisReady = " + axisReady );


	        if (axisReady){ //limit to only when key is being pressed

	                if (hAxis > 0f){//Input.GetAxis("P" + player + "_Horizontal") > 0f){
						if (currentSelection < prefab.Length - 1){
							currentSelection++;
						} else {
							currentSelection = 0;
						}
						Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Camera.main.gameObject.GetComponent<Manager>().selectClip, 0.5f);
						if (player == 1) GetComponent<DoodleAnimator>().ChangeAnimation(prefab[currentSelection].GetComponent<AnimManager>().slutIdleAnim);
						else if (player == 2) GetComponent<DoodleAnimator>().ChangeAnimation(prefab[currentSelection].GetComponent<AnimManager>().prudeIdleAnim);
						transform.localScale = prefab[currentSelection].transform.localScale;

	            		debounce = Time.realtimeSinceStartup;
	            		
					}
					
				if (hAxis < 0f)//Input.GetAxis("P" + player + "_Horizontal") < 0f)
	            {
					if (currentSelection > 0){
						currentSelection--;
					} else {
						currentSelection = prefab.Length - 1;
					}
					Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Camera.main.gameObject.GetComponent<Manager>().selectClip, 0.5f);
					//GetComponent<DoodleAnimator>().File = prefab[currentSelection].GetComponent<DoodleAnimator>().File;

					if (player == 1) GetComponent<DoodleAnimator>().ChangeAnimation(prefab[currentSelection].GetComponent<AnimManager>().slutIdleAnim);
					else if (player == 2) GetComponent<DoodleAnimator>().ChangeAnimation(prefab[currentSelection].GetComponent<AnimManager>().prudeIdleAnim);
					transform.localScale = prefab[currentSelection].transform.localScale;
	            	debounce = Time.realtimeSinceStartup;
	            	
				}
			}

			if (Input.GetButtonDown("P" + player + "_Action")){
				Spawn(false);
			}
			if (Input.GetButtonDown("P" + player + "_Next")){
				if (gameObject.name == "SlotA") {

                    aud.PlayOneShot(Camera.main.gameObject.GetComponent<Manager>().denyClip, 0.5f);
 
				} else if (gameObject.name == "SlotB") {
					Destroy(transform.parent.GetChild(transform.parent.childCount - 1).gameObject); //destroy first character
					transform.parent.GetChild(0).gameObject.SetActive(true); //turn previous on
					transform.parent.GetChild(1).gameObject.SetActive(false); //turn me off
                    aud.PlayOneShot(Camera.main.gameObject.GetComponent<Manager>().cancelClip, 1f);
				} else if (gameObject.name == "SlotC") {

					//Destroy(transform.parent.GetChild(transform.parent.childCount - 1).gameObject); //destroy first character
					Destroy(transform.parent.GetChild(transform.parent.childCount - 1).gameObject); //destroy 2nd character
					transform.parent.GetChild(1).gameObject.SetActive(true); //turn previous on
					transform.parent.GetChild(2).gameObject.SetActive(false); //turn me off

                    aud.PlayOneShot(Camera.main.gameObject.GetComponent<Manager>().cancelClip, 1f);
				}

			}
		}  
			if (Manager.numPlayers == 1 && Manager.usingBots && Manager.p1Ready){
				Spawn(true);
				Debug.Log("spawn bots");
			}
	
	}

	public void Spawn(bool rando){
			if (rando) currentSelection = Random.Range(0, prefab.Length - 1);

			Quaternion rot;
			if (player == 2){
				rot = Quaternion.identity;
			} else {
				rot = Quaternion.Euler(0f, 180f, 0f);
			}

			GameObject newbie = Instantiate(prefab[currentSelection], transform.position, rot, transform.parent);
			if (player == 2){
				newbie.GetComponent<Move>().facingRight = true;
				} else {
				newbie.GetComponent<Move>().facingRight = false;

				}
			newbie.name = prefab[currentSelection].name;

			//manually assign layers to prevent starting on different layers
			if (gameObject.name == "SlotA") {
				newbie.layer = 17;
				newbie.GetComponent<SpriteRenderer>().sortingLayerName = "Slice-9";
			}
			if (gameObject.name == "SlotB") {
				newbie.layer = 15;
				newbie.GetComponent<SpriteRenderer>().sortingLayerName = "Slice-7";
			}
			if (gameObject.name == "SlotC") {
				newbie.layer = 13;
				newbie.GetComponent<SpriteRenderer>().sortingLayerName = "Slice-5";
			}


			GameObject sib = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject;
			if (sib.CompareTag("Slot")){
				//then activate the next one
				aud.PlayOneShot(newbie.GetComponent<AudioSource>().clip, 0.5f);
				sib.SetActive(true);
			} else {
				//begin game for me
				//first turn the layers off and then back on to make the trigger layers set
				GameObject[] quints = GameObject.FindGameObjectsWithTag("Quint");

				foreach (GameObject quint in quints) {
					quint.SetActive(false);
					quint.SetActive(true);

				}

				if (sib.CompareTag("Wall")) sib.GetComponent<MoveMe>().move = true;
				sub.text = "";
				sub.transform.parent.gameObject.SetActive(false);
				uiInstructions.SetActive(false);
				if (player == 2 || !Manager.usingBots)
					instructions.SetActive(true);
					/*
				if (player == 2){
					Manager.p1Ready;
				}
				*/
			}
			if (gameObject.name == "SlotC") {
				Destroy(transform.parent.GetChild(0).gameObject); //destroy slot A
				Destroy(transform.parent.GetChild(1).gameObject); //destroy slot B
				Destroy(transform.parent.GetChild(2).gameObject); //destroy slot C
			} else {
				gameObject.SetActive(false);
			}
	}

}
