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
	public int player;

	DoodleAnimator animator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<DoodleAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (selected){
			rb.velocity = Vector2.zero;

			if (Input.GetKey(KeyCode.D)) { 
				rb.velocity += new Vector2(1.0f, 0f) * speed;
				if (!facingRight) {
					//transform.localScale *= new Vector2(-1f, 1f);

					transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
					facingRight = true;
				}
			}

			if (Input.GetKey(KeyCode.A)) { 
				rb.velocity += new Vector2(-1.0f, 0f) * speed;
				if (facingRight){
					//transform.localScale *= new Vector2(-1f, 1f);
					transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
					facingRight = false;
				} 
			}
			if (Input.GetKey(KeyCode.W)) { 
				rb.velocity += new Vector2(0f, 1f) * speed;
			}

			if (Input.GetKey(KeyCode.S)) { 
				rb.velocity += new Vector2(0f,-1f) * speed;
			}

			if (rb.velocity.magnitude > 0f){
				if (transform.childCount > 1) transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
				if (!GetComponent<Action>().doAction)  {
					if (animator.File != walkAnim) animator.File = walkAnim;
				} 
			}else {

				rb.velocity = Vector2.zero;
				if (animator.File != idleAnim && !GetComponent<Action>().doAction) animator.File = idleAnim;

			}
		
		} else {

			rb.velocity = Vector2.zero;
			if (animator.File != idleAnim && !GetComponent<Action>().doAction) animator.File = idleAnim;
		}
	}
}
