using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour {
	public bool move = false;
	public float gateThresh = 270f;
	public float gateTime = 5f;
	public AudioClip gateSound;
	public AudioSource aud;
	int player;
	// Use this for initialization
	void Start () {
		
		aud = Camera.main.GetComponent<AudioSource>();

		player = transform.parent.gameObject.GetComponent<Selection>().player;
	}
	
	// Update is called once per frame
	void Update () {
		if (move) Move();
	}

	public void Move(){
	
		if (transform.position.y < gateThresh) {
			if (!aud.isPlaying) {
				aud.clip = gateSound;
				aud.Play();
			}
			transform.position += new Vector3(0f, gateTime * Time.deltaTime, 0f);
		} else {
			//dont forget that players are swapped
			if (player == 2) Manager.p1Ready = true; else if (player == 1) Manager.p2Ready = true;
			transform.parent.gameObject.GetComponent<Selection>().Select();
			Destroy(gameObject);//.SetActive(false);
		}
	
	}
}
