using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {

	public bool doAction = false;
	public bool auto = false;
	//public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (auto){
			if (!doAction && Input.GetButton("P" + transform.parent.gameObject.GetComponent<Selection>().player + "_Action") && GetComponent<Move>().selected) {
				doAction = true;
				Play();
			}
		} else {
			if (!doAction && Input.GetButtonDown("P" + transform.parent.gameObject.GetComponent<Selection>().player + "_Action") && GetComponent<Move>().selected) {
				doAction = true;
				Play();
			}
		}
	}

	public void Play() {
		GetComponent<AnimManager>().Action();
		transform.GetChild(1).gameObject.layer = gameObject.layer;
		transform.GetChild(1).gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
		//if (!transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().isPlaying) 
		transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
	}

}
