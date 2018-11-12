using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class SwapAnim : MonoBehaviour {


	public DoodleAnimationFile prudeAnim, slutAnim;
	Transform slutParent, prudeParent;
	// Use this for initialization
	void Start () {
		slutParent = GameObject.Find("Sluts").transform;
		prudeParent = GameObject.Find("Prudes").transform;
	}
	
	// Update is called once per frame
	void Update () {
		DoodleAnimator anim = GetComponent<DoodleAnimator>();
		if (transform.parent.parent == slutParent && anim.File != slutAnim) {
			anim.ChangeAnimation(slutAnim);
			anim.enabled = false;
			anim.enabled = true;
		}
		
		if (transform.parent.parent == prudeParent && anim.File != prudeAnim) {
			anim.ChangeAnimation(prudeAnim);
			anim.enabled = false;
			anim.enabled = true;

		}
		
	}
}
