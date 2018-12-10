using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {

	bool isSlut = true;
	public float amt;
	public float max = 1f;
	public AudioClip slutConvertClip, prudeConvertClip;

	public Transform slutParent, prudeParent;
	// Use this for initialization
	void Start () {
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
			Prudify();
		} else if (amt < 0 && !isSlut) {
			Slutify();
		}

		amt = Mathf.Clamp(amt, -max, max);
	}

	public void PlayHurt(){

	}

	public void Prudify(){
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
		GetComponent<AudioSource>().PlayOneShot(slutConvertClip, 1f);

		transform.parent = slutParent;
		isSlut = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        if (GetComponent<Move>().selected){
			//Debug.Log("SLUTS WIN!");
			GetComponent<Move>().selected = false;

			prudeParent.gameObject.GetComponent<Selection>().CycleSelection();
		}
	}
}
