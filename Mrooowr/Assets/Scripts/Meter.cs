using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class Meter : MonoBehaviour {

	public bool isSlut = true;
	public float amt;
	public float max = 1f;
	public AudioClip slutConvertClip, prudeConvertClip;
	public GameObject botDeathPoof;
	public AudioClip botDeath;
	public Transform slutParent, prudeParent;
	bool botDead = false;
	float botTimeOut = 1f;
	float startTime = 0f;
	public bool fullHealth = true;
	bool convertShake = true;
	// Use this for initialization
	void Start () {
		fullHealth = true;
		if (!slutParent) slutParent = GameObject.Find("Sluts").transform;
		if (!prudeParent) prudeParent = GameObject.Find("Prudes").transform;
        if (transform.parent.gameObject.GetComponent<Selection>().player == 1)
        {
        	isSlut = true;
            amt = -1f * max;
        }
        else
        {
        	isSlut = false;
            amt = 1f * max;
            //GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (amt > 0 && isSlut) {

			if (!Manager.usingBots)		{
				Prudify();
				if (convertShake) StartCoroutine (ScreenShake.Shake (Mathf.Min(max * 0.1f, 0.4f), 0.5f));
			}
			else if (!botDead){

				if (botDeath != null) GetComponent<AudioSource>().PlayOneShot(botDeath);
				
				if (botDeathPoof != null) {
					GameObject poof = Instantiate(botDeathPoof, transform.position, Quaternion.identity) as GameObject;
					Play(poof);
				}

				//enable the next bot
				if (transform.parent.childCount != 1){
						transform.parent.GetChild(transform.parent.childCount - 1).gameObject.SetActive(true);
						transform.parent.GetChild(transform.parent.childCount - 1).SetSiblingIndex(0);
				}
				botDead = true;
				startTime = Time.time;
			} else {
				if (Time.time > botTimeOut + startTime) Destroy(gameObject);
			}
		} else if (amt < 0 && !isSlut) {
			Slutify();
				if (convertShake) StartCoroutine (ScreenShake.Shake (max * 0.1f, 0.5f));
		}

		amt = Mathf.Clamp(amt, -max, max);

		if ((amt == -max && isSlut) || (amt == max && !isSlut)) fullHealth = true;
		else fullHealth = false;
	}

	public void PlayHurt(){

	}

	public void Prudify(){
		GetComponent<AudioSource>().pitch = 1f;
		GetComponent<AudioSource>().PlayOneShot(prudeConvertClip, 0.7f);
		isSlut = false; 

		transform.parent = prudeParent;

        //GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f);
        if (GetComponent<Move>().selected){
			//Debug.Log("PRUDES WIN!");
			GetComponent<Move>().selected = false;
			slutParent.gameObject.GetComponent<Selection>().CycleSelection();
			//transform.parent.gameObject.GetComponent<Selection>().Select();
		}
	}
	public void Slutify(){
		GetComponent<AudioSource>().pitch = 1f;
		GetComponent<AudioSource>().PlayOneShot(slutConvertClip, 1f);

		transform.parent = slutParent;
		isSlut = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        if (GetComponent<Move>().selected){
			//Debug.Log("SLUTS WIN!");
			GetComponent<Move>().selected = false;

			prudeParent.gameObject.GetComponent<Selection>().CycleSelection();
		}
		if (Manager.usingBots){
			gameObject.AddComponent<Bot>();
			gameObject.GetComponent<Move>().bot = GetComponent<Bot>();
			gameObject.tag = "bot";
			gameObject.GetComponent<Move>().selected = true;
			//gameObject.GetComponent<Move>().Start();
			//gameObject.GetComponent<Bot>().enabled = true;
		}
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

			Destroy(gameObject);
    }
    animator.Stop();

  }

  public void Play(GameObject whichObj) {
    //StopAllCoroutines();
    StartCoroutine(PlaySequence(whichObj));
  }
}
