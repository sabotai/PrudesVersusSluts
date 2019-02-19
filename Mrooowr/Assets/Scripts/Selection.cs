using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoodleStudio95;

public class Selection : MonoBehaviour {

	public int player = 1;
	int selected = 1;
	public bool begun = false;
	public Text announcer;
	public GameObject selectEffect;
	public bool isBot = false;
	Vector3 oldSelect;

	// Use this for initialization
	void Start () {
		if (isBot) GetComponent<Bot>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Manager.numPlayers == 1 && player == 1){
			GetComponent<Bot>().enabled = true;//!GetComponent<Bot>().enabled;
			isBot = true;
		} 
		if (Input.GetKeyDown(KeyCode.N) && player == 1){
			GetComponent<Spawner>().enabled = !GetComponent<Spawner>().enabled;
		} 

		if ((GetComponent<Bot>().swap || Input.GetButtonDown("P" + player + "_Next")) && begun){
			if (isBot) GetComponent<Bot>().swap = false;
			CycleSelection();
		}

		if (transform.childCount == 0 && !Manager.gameOver){
			if (player == 1){
				announcer.text = Randomizer.prudeName + " Win!";
				Manager.winner = Randomizer.prudeName;
			} else {
				announcer.text = Randomizer.slutName + " Win!";
				Manager.winner = Randomizer.slutName;
			}
			Manager.gameOver = true;
		}

		if (transform.childCount > 0 && Manager.gameOver){
			Select();
			
		}

	}
	public void CycleSelection(){

		if (transform.childCount - 1 >= selected)	
			oldSelect = transform.GetChild(selected).position;

		if (selected < transform.childCount - 1) {
			selected++; 
		} else {
			selected = 0;
		}
		if (transform.childCount > 0){
			AudioSource aud = transform.GetChild(selected).gameObject.GetComponent<AudioSource>();
			aud.PlayOneShot(aud.clip);

			//have to manually run each of the single play animations because the inspector settings are giving weird results
			GameObject poof = Instantiate(selectEffect, transform.GetChild(selected).position, Quaternion.identity) as GameObject;
			//poof.GetComponent<DoodleAnimator>().Pause();
			Play(poof);

			//create the points for the select transition
			Vector3[] pos = {transform.GetChild(selected).position, oldSelect};
			//GetComponent<LineRenderer>().positions[0] = transform.GetChild(selected).position;
			//GetComponent<LineRenderer>().positions[1] = transform.GetChild(oldSelect).position;	
			
			//Debug.Log("selecting " + transform.GetChild(selected).gameObject.name + " from " + transform.GetChild(oldSelect).gameObject.name);
			GetComponent<DoodleAnimator>().Pause();
			GetComponent<LineRenderer>().SetPositions(pos);
			//GetComponent<DoodleAnimator>().PlayAndPauseAt(0, -1);
			Play(gameObject);

		}
		Select();
	}
	public void Select(){
		if (!begun){
			if (transform.GetChild(0).gameObject.tag == "Wall") transform.GetChild(1).gameObject.GetComponent<Move>().selected = true;
 			else transform.GetChild(0).gameObject.GetComponent<Move>().selected = true;
				//disable name label
			for(int i = 1; i < transform.childCount; i++)
				if (i != selected) transform.GetChild(i).GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().enabled = false;
				else  transform.GetChild(i).GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().enabled = true;
			
			begun = true;
		}

		for(int i = 0; i < transform.childCount; i++){
			if (!transform.GetChild(0).gameObject.CompareTag("Slot") && !transform.GetChild(0).gameObject.CompareTag("Wall")){
				if (!Manager.gameOver){
					if (i != selected) {
						transform.GetChild(i).gameObject.GetComponent<Move>().selected = false;
						//disable name label
						transform.GetChild(i).GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().enabled = false;
					}

				}
				else {//select all when game over
					transform.GetChild(i).gameObject.GetComponent<Move>().selected = true;
					//enable name label
					transform.GetChild(i).GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().enabled = true;
				}
			}
		}
		if (transform.childCount > 0 && transform.GetChild(selected).gameObject.CompareTag("Characters"))	{
				transform.GetChild(selected).gameObject.GetComponent<Move>().selected = true;
				//enable name label
				transform.GetChild(selected).GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().enabled = true;
			}

		//if (transform.GetChild(selected))	
		//	oldSelect = transform.GetChild(selected).position;

	}


  IEnumerator PlaySequence(GameObject _whichObj) {
    DoodleAnimator animator = _whichObj.GetComponent<DoodleAnimator>();
    int i = 0;
    animator.Pause();
    while(i < 1) {
      // Play the animation and wait until it's finished
      yield return animator.PlayAndPauseAt();
      // Advanced to the next animation
      i++;
    }
    animator.Stop();
  }

  public void Play(GameObject whichObj) {
    //StopAllCoroutines();
    StartCoroutine(PlaySequence(whichObj));
  }
}
