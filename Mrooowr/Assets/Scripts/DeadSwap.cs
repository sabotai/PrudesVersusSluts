using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class DeadSwap : MonoBehaviour {
	public DoodleAnimationFile dead, dead2, dead3, dead4, dead5, dead6;
	public bool move = true;
	public bool isDead = false;
	public bool isRat = true;
	public Vector3 direction;
	public float speed = 1f;
	public AudioClip deadClip;
	public int minHits = 1;
	public int numHits = 0;
	public bool goBoom = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			if (move){
				//transform.position += direction * (speed * Random.value) * Time.deltaTime + new Vector3(0f, Mathf.Sin(Time.time * 2f) * 0.005f * Random.value, 0f);

				GetComponent<Rigidbody2D>().freezeRotation = true;
				GetComponent<Rigidbody2D>().velocity = direction * (speed * Random.value) * Time.deltaTime;
				//transform.position += new Vector3(0f, Mathf.Sin(Time.time * 2f) * 0.005f * Random.value, 0f);
				GetComponent<Rigidbody2D>().position += new Vector2(0f, Mathf.Sin(Time.time * 2f) * 0.005f * Random.value);
			}
		} else {
			if (isRat) GetComponent<Rigidbody2D>().freezeRotation = false;
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		numHits++;
		if (numHits >= minHits){
			if (col.gameObject.CompareTag("Characters")) {

				if (GetComponent<DoodleAnimator>().File == dead && dead2 != null){
					GetComponent<DoodleAnimator>().ChangeAnimation(dead2);
					GetComponent<AudioSource>().PlayOneShot(deadClip, 0.1f);
					if (goBoom)	StartCoroutine (ScreenShake.Shake (0.05f, 0.5f));
					numHits = 0;
					minHits = 1;

				} else if (GetComponent<DoodleAnimator>().File == dead2 && dead3 != null){
					GetComponent<DoodleAnimator>().ChangeAnimation(dead3);
					GetComponent<AudioSource>().PlayOneShot(deadClip, 0.1f);
					if (goBoom)	StartCoroutine (ScreenShake.Shake (0.05f, 0.5f));
					numHits = 0;
					minHits = 1;


				}  else if (GetComponent<DoodleAnimator>().File == dead3 && dead4 != null){
					GetComponent<DoodleAnimator>().ChangeAnimation(dead4);
					GetComponent<AudioSource>().PlayOneShot(deadClip, 0.1f);
					if (goBoom)	StartCoroutine (ScreenShake.Shake (0.05f, 0.25f));
					numHits = 0;
					minHits = 1;


				}  else if (GetComponent<DoodleAnimator>().File == dead4 && dead5 != null){
					GetComponent<DoodleAnimator>().ChangeAnimation(dead5);
					GetComponent<AudioSource>().PlayOneShot(deadClip, 0.1f);
					if (goBoom)	StartCoroutine (ScreenShake.Shake (0.05f, 0.1f));
					numHits = 0;
					minHits = 1;


				}   else if (GetComponent<DoodleAnimator>().File == dead5 && dead6 != null){
					GetComponent<DoodleAnimator>().ChangeAnimation(dead6);
					GetComponent<AudioSource>().PlayOneShot(deadClip, 0.6f);
					if (goBoom)	StartCoroutine (ScreenShake.Shake (0.3f, 0.5f));
					numHits = 0;
					minHits = 3;


				} else if (GetComponent<DoodleAnimator>().File != dead6 && GetComponent<DoodleAnimator>().File != dead){
					GetComponent<DoodleAnimator>().ChangeAnimation(dead);
					numHits = 0;
					GetComponent<AudioSource>().PlayOneShot(deadClip, 0.5f);
					if (goBoom)	StartCoroutine (ScreenShake.Shake (0.3f, 0.5f));
					isDead = true;

				}
			}


		}
	}
}
