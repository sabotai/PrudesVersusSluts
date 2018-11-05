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
					transform.localScale *= new Vector2(-1f, 1f);
					facingRight = true;
				}
			}

			if (Input.GetKey(KeyCode.A)) { 
				rb.velocity += new Vector2(-1.0f, 0f) * speed;
				if (facingRight){
					transform.localScale *= new Vector2(-1f, 1f);
					facingRight = false;
				} 
			}
			if (Input.GetKey(KeyCode.W)) { 
				rb.velocity += new Vector2(0f, 1f) * speed;
			}

			if (Input.GetKey(KeyCode.S)) { 
				rb.velocity += new Vector2(0f,-1f) * speed;
			}
			if (rb.velocity.magnitude > 0f)  {
				if (animator.File != walkAnim) animator.File = walkAnim;
			}
		
		} else {

			rb.velocity = Vector2.zero;
			if (animator.File != idleAnim) animator.File = idleAnim;
		}
	}
}
