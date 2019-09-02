using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class AnimManager : MonoBehaviour {
	int player;
	public DoodleAnimationFile slutActionAnim, prudeActionAnim;
	public DoodleAnimationFile slutHurtAnim, prudeHurtAnim, slutGoodHurtAnim, prudeGoodHurtAnim;
	public DoodleAnimationFile slutIdleAnim, prudeIdleAnim; 
	public DoodleAnimationFile slutWalkAnim, prudeWalkAnim; 
	public GameObject prudeSwapEffect, slutSwapEffect;
	public bool animReady = true;
	DoodleAnimator animator;
	public float animTimeOut = 2f;
	float startTime = 0f;
	public string namePrude, nameSlut;

	// Use this for initialization
	void Start () {
		animator = GetComponent<DoodleAnimator>();
		if (transform.parent.gameObject.GetComponent<Selection>().player == 1) {
			player = 1;
			gameObject.name = nameSlut;
		} else {
			player = 2;
			gameObject.name = namePrude;
		}
		if (player == 1) animator.ChangeAnimation(slutIdleAnim);
		else if (player == 2) animator.ChangeAnimation(prudeIdleAnim);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.gameObject.GetComponent<Selection>().player == 1 && player != 1) {
			player = 1;
			gameObject.name = nameSlut;

			//have to manually run each of the single play animations because the inspector settings are giving weird results
			GameObject poof = Instantiate(slutSwapEffect, transform.position, Quaternion.identity) as GameObject;
			//poof.GetComponent<DoodleAnimator>().Pause();
			Play(poof);
		} else if (transform.parent.gameObject.GetComponent<Selection>().player == 2 && player != 2) {
			player = 2;
			gameObject.name = namePrude;

			//have to manually run each of the single play animations because the inspector settings are giving weird results
			GameObject poof = Instantiate(prudeSwapEffect, transform.position, Quaternion.identity) as GameObject;
			//poof.GetComponent<DoodleAnimator>().Pause();
			Play(poof);
		}

		if (Time.time > startTime + animTimeOut && !animReady) animReady = true; 
	}

	public void Walk(){
		if (animator.File != slutWalkAnim && animator.File != prudeWalkAnim){
			StopAllCoroutines();
			GetComponent<Hurt>().doHurt = false;
			GetComponent<Hurt>().hurting = false;
			GetComponent<Action>().doAction = false;
			animReady = true;

			//if (player == 1) StartCoroutine(PlaySequence(slutWalkAnim)); 
			//else if (player == 2) StartCoroutine(PlaySequence(prudeWalkAnim)); 
			if (player == 1) animator.ChangeAnimation(slutWalkAnim); 
			else if (player == 2) animator.ChangeAnimation(prudeWalkAnim); 
			animator.Play();
		}
	}

	public void Idle(){
		if (animator.File != slutIdleAnim && animator.File != prudeIdleAnim){
			StopAllCoroutines();
			GetComponent<Hurt>().doHurt = false;
			GetComponent<Hurt>().hurting = false;
			GetComponent<Action>().doAction = false;
			animReady = true;

			if (player == 1) animator.ChangeAnimation(slutIdleAnim); 
			else if (player == 2) animator.ChangeAnimation(prudeIdleAnim); 
			animator.Play();
		}

	}

	public void Action(){
		StopAllCoroutines();

		GetComponent<Hurt>().doHurt = false;
		GetComponent<Hurt>().hurting = false;
		animReady = false;
		startTime = Time.time;

		if (player == 1) StartCoroutine(PlaySequence(slutActionAnim)); 
		else if (player == 2) StartCoroutine(PlaySequence(prudeActionAnim)); 
	}
	public void Hurt(){
		StopAllCoroutines();
		Debug.Log("ouch " + gameObject.name);
		GetComponent<Action>().doAction = false;
		animReady = false;
		startTime = Time.time;

		if (player == 1) StartCoroutine(PlaySequence(slutHurtAnim)); 
		else if (player == 2) StartCoroutine(PlaySequence(prudeHurtAnim)); 
	}
	public void GoodHurt(){
		StopAllCoroutines();
		Debug.Log("ooo " + gameObject.name);
		GetComponent<Action>().doAction = false;
		animReady = false;
		startTime = Time.time;

		if (player == 1) StartCoroutine(PlaySequence(slutGoodHurtAnim)); 
		else if (player == 2) StartCoroutine(PlaySequence(prudeGoodHurtAnim)); 
	}

	IEnumerator PlaySequence(DoodleAnimationFile anim) {
			int i = 0;
			while(i < 1) {
				// Set the new animation
				animator.ChangeAnimation(anim);
				// Play the animation and wait until it's finished
				yield return animator.PlayAndPauseAt();
				// Advanced to the next animation
				i++;
			}
		

		if (player == 1) animator.ChangeAnimation(slutIdleAnim);
		else if (player == 2) animator.ChangeAnimation(prudeIdleAnim);
		
		animReady = true;
		if (anim == slutActionAnim || anim == prudeActionAnim){
			GetComponent<Action>().doAction = false;
		} else if (anim == slutHurtAnim || anim == prudeHurtAnim || anim == slutGoodHurtAnim || anim == prudeGoodHurtAnim){
			GetComponent<Hurt>().doHurt = false;
			GetComponent<Hurt>().goodHurt = false;
			GetComponent<Hurt>().hurting = false;

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
    }
    animator.Stop();
  }

  public void Play(GameObject whichObj) {
    //StopAllCoroutines();
    StartCoroutine(PlaySequence(whichObj));
  }
}
