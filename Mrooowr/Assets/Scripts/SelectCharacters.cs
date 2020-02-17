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
	public AudioClip selectSound;
	AudioSource aud;
	public GameObject instructions;
	Bot bot;


    float debounce = 0f;
    public float repeat = 0.2f;  // reduce to speed up auto-repeat input
    bool axisReady = true;

    // Use this for initialization
    void Start () {
		player = transform.parent.gameObject.GetComponent<Selection>().player;
		sub.text = "Choose 3 " + Randomizer.prudeName + "(P1) / " + Randomizer.slutName + "(P2)!";
		aud = Camera.main.GetComponent<AudioSource>();
		//GetComponent<DoodleAnimator>().ChangeAnimation(prefab[currentSelection].GetComponent<DoodleAnimator>().File;
		transform.localScale = prefab[currentSelection].transform.localScale;
		bot = GetComponent<Bot>();
	}
	
	// Update is called once per frame
	void Update ()
    {

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
        Debug.Log("vAxis = " + vAxis + " // hAxis = " + hAxis + " // axisReady = " + axisReady );

/*

        float vAxis1 = Input.GetAxis("P1_Vertical");
        float hAxis1 = Input.GetAxis("P1_Horizontal"); 
        float vAxis2 = Input.GetAxis("P2_Vertical");
        float hAxis2 = Input.GetAxis("P2_Horizontal");                                                   //bot
        float now = Time.realtimeSinceStartup;
        // check if user let go of the stick; if so, reset the input bounce control
        if (vAxis1 != 0f && hAxis1 != 0f)
        {
            debounce1 = 0f; //resets debounce
        }
        if (vAxis2 != 0f && hAxis2 != 0f)
        {
            debounce2 = 0f; //resets debounce
        }
        // if it's been long enough since the last input, then we allow it
        if (now - debounce1 > repeat)
        {
            axisReady1 = true;
        }
        else
        {
            axisReady1 = false;
        }

        // if it's been long enough since the last input, then we allow it
        if (now - debounce2 > repeat)
        {
            axisReady2 = true;
            debounce2 = Time.realtimeSinceStartup;
        }
        else
        {
            axisReady2 = false;
        }
        */



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
				instructions.SetActive(false);
			}
			if (Manager.numPlayers == 1 && bot) bot.enabled = true;
			Destroy(gameObject);//.SetActive(false);
		}
	
	}

}
