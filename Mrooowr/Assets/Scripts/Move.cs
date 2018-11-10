using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class Move : MonoBehaviour {

	Rigidbody2D rb;
	public bool selected = true;
	public float speed = 2f;
	public bool facingRight = true;
	public DoodleAnimationFile idleAnim, walkAnim;
	public DoodleAnimationFile slutIdleAnim, slutWalkAnim;
	public DoodleAnimationFile prudeIdleAnim, prudeWalkAnim;
	public int player;
	AudioSource aud;

	DoodleAnimator animator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<DoodleAnimator>();
		if (!slutIdleAnim) slutIdleAnim = idleAnim;
		if (!slutWalkAnim) slutWalkAnim = walkAnim;
		if (!prudeIdleAnim) prudeIdleAnim = idleAnim;
		if (!prudeWalkAnim) prudeWalkAnim = walkAnim;
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Meter>().isSlut) player = 1; else player = 2;

		if (selected && !GetComponent<Action>().doAction){
			rb.velocity = Vector2.zero;

			//if (Input.GetKey(KeyCode.D)) { 
			if (Input.GetAxis("P" + player + "_Horizontal") > 0f){
				rb.velocity += new Vector2(1.0f, 0f) * speed;
				if (!facingRight) {
					//transform.localScale *= new Vector2(-1f, 1f);
					transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Labels>().FixRot();
					transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
					facingRight = true;
				}
			}

			if (Input.GetAxis("P" + player + "_Horizontal") < 0f){
				rb.velocity += new Vector2(-1.0f, 0f) * speed;
				if (facingRight){
					//transform.localScale *= new Vector2(-1f, 1f);
					transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Labels>().FixRot();
					transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
					facingRight = false;
				} 
			}
			if (Input.GetAxis("P" + player + "_Vertical") > 0f){
				rb.velocity += new Vector2(0f, 1f) * speed;
			}

			if (Input.GetAxis("P" + player + "_Vertical") < 0f){
				rb.velocity += new Vector2(0f,-1f) * speed;
			}

			if (rb.velocity.magnitude > 0f){
				if (!aud.isPlaying) aud.Play();
				if (transform.childCount > 1) transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
				if (!GetComponent<Action>().doAction)  {
					if (animator.File != walkAnim) animator.File = walkAnim;
				} 
			}else {
				aud.Stop();
				rb.velocity = Vector2.zero;
				if (animator.File != idleAnim && !GetComponent<Action>().doAction) animator.File = idleAnim;

			}
		
		} else {

			aud.Stop();
			rb.velocity = Vector2.zero;
			if (animator.File != idleAnim && !GetComponent<Action>().doAction) animator.File = idleAnim;
		}
	}

	public void SetAnim(string anim){
		if (anim == "slut") {
			idleAnim = slutIdleAnim;
			walkAnim = slutWalkAnim;
		}
		if (anim == "prude") {
			idleAnim = prudeIdleAnim;
			walkAnim = prudeWalkAnim;
		}
	}
}
