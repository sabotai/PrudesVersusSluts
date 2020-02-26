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
	public bool stopActionAudioWhileWalking = true;
	bool walking = false;
	public bool stopForAction = false;
	public Bot bot;
	public bool moveCancelsPS = true;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		aud = GetComponent<AudioSource>();
		player = transform.parent.gameObject.GetComponent<Selection>().player;
		if (GetComponent<Bot>() != null){
			bot = GetComponent<Bot>();
			if (Manager.numPlayers == 1 && player == 1) {
				bot.enabled = true;
			}
			else {
				bot.enabled = false;
				//Destroy(bot);// = null;
				//bot = null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		player = transform.parent.gameObject.GetComponent<Selection>().player;
		walking = false; //starts out false until proven true
		if (selected && !GetComponent<Hurt>().doHurt){
			rb.velocity = Vector2.zero;

			if (!stopForAction || bot.enabled){
				if (Input.GetAxis("P" + player + "_Horizontal") > 0f

					||  (bot.enabled && bot.hori > 0f)){
					walking = true;
					rb.velocity += new Vector2(1.0f, 0f) * speed;
					if (!facingRight) {
						//transform.localScale *= new Vector2(-1f, 1f);
						transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Labels>().FixRot();
						transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
						facingRight = true;
					}
				}

				if (Input.GetAxis("P" + player + "_Horizontal") < 0f
					||  (bot.enabled && bot.hori < 0f)){

					walking = true;
					rb.velocity += new Vector2(-1.0f, 0f) * speed;
					if (facingRight){
						//transform.localScale *= new Vector2(-1f, 1f);
						transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Labels>().FixRot();
						transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
						facingRight = false;
					} 
				}
				if (Input.GetAxis("P" + player + "_Vertical") > 0f
					||  (bot.enabled && bot.vert > 0f)){

					walking = true;
					rb.velocity += new Vector2(0f, 1f) * speed;
				}

				if (Input.GetAxis("P" + player + "_Vertical") < 0f
					||  (bot.enabled && bot.vert < 0f)){

					walking = true;
					rb.velocity += new Vector2(0f,-1f) * speed;
				}
				if (stopActionAudioWhileWalking && walking)
					transform.GetChild(1).gameObject.GetComponent<AudioSource>().Stop();


				if (rb.velocity.magnitude > 0f){ //if player is moving
					if (!aud.isPlaying && playWalkSound) {
						//aud.clip = defClip;
						Debug.Log(gameObject.name + " walking sound");
						aud.pitch = 1f;
						aud.Play();
					}

					//cancel the actions particle system
					if (transform.childCount > 1 && moveCancelsPS) transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
					
					//do a walk anim
					GetComponent<AnimManager>().Walk();

					
					//trying to fix the glitch where the heal audio keeps playing
					if (walking){						
						GetComponent<Hurt>().hurting = false;
						GetComponent<Hurt>().doHurt = false;
						GetComponent<Hurt>().goodHurt = false;
					}
					
					
				} else {
					if (playWalkSound) aud.Stop();
					rb.velocity = Vector2.zero;
					if (GetComponent<AnimManager>().animReady)// !GetComponent<Action>().doAction && !GetComponent<Hurt>().doHurt) 
						GetComponent<AnimManager>().Idle();//animator.ChangeAnimation(idleAnim);

				}
			} else { //stopped to do an action
				if (Input.GetAxis("P" + player + "_Vertical") == 0f && Input.GetAxis("P" + player + "_Horizontal") == 0f){
					stopForAction = false;

					/*
					//trying to fix the glitch where the heal audio keeps playing
					GetComponent<Hurt>().hurting = false;
					GetComponent<Hurt>().doHurt = false;
					GetComponent<Hurt>().goodHurt = false;
					*/
				}
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
