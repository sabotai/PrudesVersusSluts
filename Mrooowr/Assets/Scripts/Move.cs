using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class Move : MonoBehaviour {

	Rigidbody2D rb;
	public bool selected = true;
	public float speed = 2f;
	public bool facingRight = true;
	int player;
	AudioSource aud;
	DoodleAnimator animator;
	public bool playWalkSound = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		aud = GetComponent<AudioSource>();
		player = transform.parent.gameObject.GetComponent<Selection>().player;
	}
	
	// Update is called once per frame
	void Update () {
		player = transform.parent.gameObject.GetComponent<Selection>().player;

		if (selected && !GetComponent<Hurt>().doHurt){
			rb.velocity = Vector2.zero;

			//if (Input.GetKey(KeyCode.D)) { 
			if (Input.GetAxis("P" + player + "_Horizontal") > 0f
				|| transform.parent.gameObject.GetComponent<Bot>().hori > 0f){
				rb.velocity += new Vector2(1.0f, 0f) * speed;
				if (!facingRight) {
					//transform.localScale *= new Vector2(-1f, 1f);
					transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Labels>().FixRot();
					transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
					facingRight = true;
				}
			}

			if (Input.GetAxis("P" + player + "_Horizontal") < 0f
				|| transform.parent.gameObject.GetComponent<Bot>().hori < 0f){
				rb.velocity += new Vector2(-1.0f, 0f) * speed;
				if (facingRight){
					//transform.localScale *= new Vector2(-1f, 1f);
					transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Labels>().FixRot();
					transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
					facingRight = false;
				} 
			}
			if (Input.GetAxis("P" + player + "_Vertical") > 0f
				|| transform.parent.gameObject.GetComponent<Bot>().vert > 0f){
				rb.velocity += new Vector2(0f, 1f) * speed;
			}

			if (Input.GetAxis("P" + player + "_Vertical") < 0f
				|| transform.parent.gameObject.GetComponent<Bot>().vert < 0f){
				rb.velocity += new Vector2(0f,-1f) * speed;
			}

			if (rb.velocity.magnitude > 0f){
				if (!aud.isPlaying && playWalkSound) {
					//aud.clip = defClip;
					Debug.Log(gameObject.name + " walking sound");
					aud.Play();
				}
				if (transform.childCount > 1) transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
				//if (GetComponent<AnimManager>().animReady){// && !GetComponent<Action>().doAction && !GetComponent<Hurt>().doHurt )  {
					GetComponent<AnimManager>().Walk();//animator.ChangeAnimation(walkAnim);
				//} 
			} else {
				if (playWalkSound) aud.Stop();
				rb.velocity = Vector2.zero;
				if (GetComponent<AnimManager>().animReady)// !GetComponent<Action>().doAction && !GetComponent<Hurt>().doHurt) 
					GetComponent<AnimManager>().Idle();//animator.ChangeAnimation(idleAnim);

			}
		
		} else {
			rb.velocity = Vector2.zero;
			if (GetComponent<AnimManager>().animReady){// && !GetComponent<Action>().doAction && !GetComponent<Hurt>().doHurt) {
				//animator.ChangeAnimation(idleAnim);
				GetComponent<AnimManager>().Idle();//animator.ChangeAnimation(idleAnim);

				if (playWalkSound) aud.Stop();
			}
		}
	}

}
