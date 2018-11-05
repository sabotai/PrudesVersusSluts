using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class Action : MonoBehaviour {
	DoodleAnimationFile origAnim;
	public DoodleAnimationFile actionAnim;

	public bool doAction = false;
	//public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("P1_Action")) doAction = true;
		if (doAction){

			Play();
			doAction = false;
		}


	}

	IEnumerator PlaySequence() {
		DoodleAnimator animator = GetComponent<DoodleAnimator>();
		int i = 0;
		while(i < 1) {
			GetComponent<Move>().selected = false;
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
			GetComponent<Move>().selected = true;
		}
		animator.ChangeAnimation(origAnim);
		//doAction = false;
		//animator.Stop();
	}

	public void Play() {
		StopAllCoroutines();
		StartCoroutine(PlaySequence());
	}
}
