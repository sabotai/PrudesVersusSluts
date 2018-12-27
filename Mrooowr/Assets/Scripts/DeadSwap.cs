using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class DeadSwap : MonoBehaviour {
	public DoodleAnimationFile dead;
	public bool isDead = false;
	public Vector3 direction;
	public float speed = 1f;
	public AudioClip deadClip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			//transform.position += direction * (speed * Random.value) * Time.deltaTime + new Vector3(0f, Mathf.Sin(Time.time * 2f) * 0.005f * Random.value, 0f);

			GetComponent<Rigidbody2D>().freezeRotation = true;
			GetComponent<Rigidbody2D>().velocity = direction * (speed * Random.value) * Time.deltaTime;
			//transform.position += new Vector3(0f, Mathf.Sin(Time.time * 2f) * 0.005f * Random.value, 0f);
			GetComponent<Rigidbody2D>().position += new Vector2(0f, Mathf.Sin(Time.time * 2f) * 0.005f * Random.value);
		} else {
			GetComponent<Rigidbody2D>().freezeRotation = false;
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag("Characters") && !isDead) {
			GetComponent<DoodleAnimator>().ChangeAnimation(dead);
			GetComponent<AudioSource>().PlayOneShot(deadClip, 0.6f);
			isDead = true;
		}
	}
}
