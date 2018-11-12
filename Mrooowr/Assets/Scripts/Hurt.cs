using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class Hurt : MonoBehaviour {
	DoodleAnimationFile origAnim;
	public DoodleAnimationFile hurtAnim, slutHurtAnim, prudeHurtAnim;
	public AudioClip hurtClip;
	public bool doHurt = false;
	public bool hurting = false;
	//public 
	// Use this for initialization
	void Start () {
		if (transform.parent.gameObject.GetComponent<Selection>().player == 1) {
			SetAnim("slut");
		} else {
			SetAnim("prude");
		}
		if (!slutHurtAnim) slutHurtAnim = hurtAnim;
		if (!prudeHurtAnim) prudeHurtAnim = hurtAnim;
		
	}
	void Update(){


		if (doHurt && !hurting) {
			GetComponent<Action>().doAction = false;
			hurting = true;
			Debug.Log(gameObject.name + " hurting");
			//GetComponent<AudioSource>().clip = hurtClip;
			//GetComponent<AudioSource>().Play();
			GetComponent<AudioSource>().PlayOneShot(hurtClip, 0.5f);
			Play();
		} else {
			
			if (transform.parent.gameObject.GetComponent<Selection>().player == 1) {
				SetAnim("slut");
			} else {
				SetAnim("prude");
			}
			

		}
	}

	IEnumerator PlaySequence() {
		DoodleAnimator animator = GetComponent<DoodleAnimator>();
		
		int i = 0;
		while(i < 1) {
			origAnim = GetComponent<DoodleAnimator>().File;
			//GetComponent<Move>().selected = false;
			// Set the new animation
			animator.ChangeAnimation(hurtAnim);
			// Play the animation and wait until it's finished
			yield return animator.PlayAndPauseAt();
			// Advanced to the next animation
			i++;
			// Loop if we've reached the end
			//if (i >= m_Animations.Length && m_Loop)
			//	i = 0;
			//GetComponent<Move>().selected = true;
		}
		Debug.Log("done looping...");
		animator.ChangeAnimation(origAnim);
		//animator.Play();
		doHurt = false;
		hurting = false;
		//animator.Stop();
	}

	public void Play() {
		StopAllCoroutines();
		if (hurtAnim)		{
			StartCoroutine(PlaySequence()); 
		} else {
			doHurt = false;
		}
		//transform.GetChild(1).gameObject.layer = gameObject.layer;
		//.GetChild(1).gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
		//if (!transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().isPlaying) 
		//transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
	}

	public void SetAnim(string anim){

		//origAnim = GetComponent<Move>().idleAnim;
		if (anim == "slut") {
			hurtAnim = slutHurtAnim;
		}
		if (anim == "prude") {
			hurtAnim = prudeHurtAnim;
		}
	}
}
