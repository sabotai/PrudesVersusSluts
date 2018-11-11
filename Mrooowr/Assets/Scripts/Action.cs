using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class Action : MonoBehaviour {
	DoodleAnimationFile origAnim;
	public DoodleAnimationFile actionAnim, slutActionAnim, prudeActionAnim;
	public AudioClip actionClip;
	public bool doAction = false;
	public bool auto = false;
	//public 
	// Use this for initialization
	void Start () {
		if (transform.parent.gameObject.GetComponent<Selection>().player == 1) {
			SetAnim("slut");
		} else {
			SetAnim("prude");
		}
		if (!slutActionAnim) slutActionAnim = actionAnim;
		if (!prudeActionAnim) prudeActionAnim = actionAnim;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (auto){
			if (!doAction && !GetComponent<Hurt>().doHurt && Input.GetButton("P" + transform.parent.gameObject.GetComponent<Selection>().player + "_Action") && GetComponent<Move>().selected) {
				doAction = true;
				Play();
			}
		} else {
			if (!doAction && !GetComponent<Hurt>().doHurt && Input.GetButtonDown("P" + transform.parent.gameObject.GetComponent<Selection>().player + "_Action") && GetComponent<Move>().selected) {
				doAction = true;
				Play();
			}
		}
		if (doAction) GetComponent<AudioSource>().PlayOneShot(actionClip, 0.1f);

	}

	IEnumerator PlaySequence() {
		DoodleAnimator animator = GetComponent<DoodleAnimator>();
		int i = 0;
		while(i < 1) {
			//GetComponent<Move>().selected = false;
			origAnim = GetComponent<DoodleAnimator>().File;
			// Set the new animation
			animator.ChangeAnimation(actionAnim);
			// Play the animation and wait until it's finished
			yield return animator.PlayAndPauseAt();
			// Advanced to the next animation
			i++;
			// Loop if we've reached the end
			//if (i >= m_Animations.Length && m_Loop)
			//	i = 0;
			//GetComponent<Move>().selected = true;
		}
		animator.ChangeAnimation(origAnim);
		doAction = false;
		//animator.Stop();
	}

	public void Play() {
		StopAllCoroutines();
		if (actionAnim)		{
			StartCoroutine(PlaySequence()); 
		} else {
			doAction = false;
		}
		transform.GetChild(1).gameObject.layer = gameObject.layer;
		transform.GetChild(1).gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
		//if (!transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().isPlaying) 
		transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
	}

	public void SetAnim(string anim){
		if (anim == "slut") {
			actionAnim = slutActionAnim;
		}
		if (anim == "prude") {
			actionAnim = prudeActionAnim;
		}
	}
}
