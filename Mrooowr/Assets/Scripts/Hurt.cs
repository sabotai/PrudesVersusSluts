using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour {
	public AudioClip hurtClip;
	public bool doHurt = false;
	public bool hurting = false;
	// Use this for initialization
	void Start () {
		
	}
	void Update(){


		if (doHurt && !hurting) {
			GetComponent<Action>().doAction = false;
			hurting = true;
			Debug.Log(gameObject.name + " hurting");
			//GetComponent<AudioSource>().clip = hurtClip;
			//GetComponent<AudioSource>().Play();
			GetComponent<AudioSource>().PlayOneShot(hurtClip, 1.2f);
			Play();
		} 
	}


	public void Play() {
		GetComponent<AnimManager>().Hurt();
	}

}
