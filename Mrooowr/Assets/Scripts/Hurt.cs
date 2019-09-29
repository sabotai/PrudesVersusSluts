using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour {
	public AudioClip[] hurtClip;
	public AudioClip healClip;
	public bool doHurt = false;
	public bool goodHurt = false;
	public bool hurting = false;
	public float hurtAmt = 1f;
	// Use this for initialization
	void Start () {
		
	}
	void Update(){


		if ((goodHurt || doHurt) && !hurting) {
			GetComponent<Action>().doAction = false;
			hurting = true;
			//GetComponent<AudioSource>().clip = hurtClip;
			//GetComponent<AudioSource>().Play();
			AudioSource aud = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<AudioSource>();
			aud.pitch = hurtAmt;
			AudioClip clip;
			if (goodHurt) clip = healClip;
			else clip = hurtClip[Random.Range(0,hurtClip.Length)];
			if (goodHurt) {		
				//for goodHurt, use regular play so it doesnt glitch out
				aud.clip = clip;
				aud.volume = Mathf.Min(0.6f, 1.5f - hurtAmt);
				aud.Play();
				aud.PlayOneShot(hurtClip[Random.Range(0,hurtClip.Length)], 0.17f); //sounded weird without a collision sound

			} else {
				//just play regular hurt
				aud.PlayOneShot(clip, Mathf.Min(0.6f, 1.5f - hurtAmt));
			}
			Play();

			//GetComponent<AudioSource>().pitch = 1f;
		} 

		hurtAmt = 1f;
	}


	public void Play() {
		if (goodHurt) GetComponent<AnimManager>().GoodHurt();
		 else GetComponent<AnimManager>().Hurt();
	}

}
