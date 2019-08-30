using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {

	public bool doAction = false;
	public bool auto = false;
    AudioClip emitClip;
    public AudioClip emitClipPrude, emitClipSlut;
    AudioSource aud;
	//public 
	// Use this for initialization
	void Start () {
        aud = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent.GetComponent<Selection>().player == 1) emitClip = emitClipSlut;
        else  emitClip = emitClipPrude;

        //disabled this because it was stopping the full sneeze sound
        //...cant remember why i had this to begin with
        //if (!doAction) aud.Stop();

        //auto for eggy and anyone else who gets the auto
		if (auto && !doAction 
			&& (Input.GetButton("P" + transform.parent.gameObject.GetComponent<Selection>().player + "_Action") 
				|| (GetComponent<Bot>() && GetComponent<Bot>().attack))
			&& GetComponent<Move>().selected) {
				if (GetComponent<Bot>()) GetComponent<Bot>().attack = false;
				doAction = true;
    			ActionSounds();
				Play();
			
		} else if (!doAction 
			&& (Input.GetButtonDown("P" + transform.parent.gameObject.GetComponent<Selection>().player + "_Action") 
				|| (GetComponent<Bot>() && GetComponent<Bot>().attack))
			&& GetComponent<Move>().selected) {
				if (GetComponent<Bot>()) GetComponent<Bot>().attack = false;
				doAction = true;
    			ActionSounds();
				Play();
			
		} else {

    		aud.loop = false;
		}
	}
    void ActionSounds(){
		//if (!aud.isPlaying){
			aud.clip = emitClip;
			aud.loop = true;
			aud.Play();
			//aud.PlayDelayed(0.1f);//PlayOneShot(actionClip, 0.1f);
		//}	
    }

	public void Play() {
		GetComponent<AnimManager>().Action();
		transform.GetChild(1).gameObject.layer = gameObject.layer;
		transform.GetChild(1).gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
		//if (!transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().isPlaying) 
		transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
	}

}
