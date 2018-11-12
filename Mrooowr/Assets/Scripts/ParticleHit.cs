using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour {

    ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float partDmg = 0f;
    public AudioClip emitClip;

    AudioSource aud;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        aud = GetComponent<AudioSource>();
    }

    void Update(){
    	if (part.particleCount > 0 && part.isEmitting) {
    		ActionSounds();
    	} else {
    		aud.loop = false;
    		//Stop();
    	}


    	var col = part.collision;
    	int newLayer = transform.parent.gameObject.layer;

    	//Debug.Log(transform.parent.gameObject.name + " newLayer for particles = " + newLayer);
    	if (newLayer == 8){    		
    		col.collidesWith = LayerMask.GetMask(LayerMask.LayerToName(newLayer), LayerMask.LayerToName(newLayer + 1), "Bounds");
     	} else if (newLayer == 18){
    		col.collidesWith = LayerMask.GetMask(LayerMask.LayerToName(newLayer), LayerMask.LayerToName(newLayer - 1), "Bounds");
 
    	} else if (newLayer >= 8) {
    		col.collidesWith = LayerMask.GetMask(LayerMask.LayerToName(newLayer), LayerMask.LayerToName(newLayer - 1), LayerMask.LayerToName(newLayer + 1), "Bounds");
    	}
   	
    }
    void ActionSounds(){
		if (!aud.isPlaying){
			aud.clip = emitClip;
			aud.loop = true;
			aud.Play();
			//aud.PlayDelayed(0.1f);//PlayOneShot(actionClip, 0.1f);
		}	
    }

    void OnParticleCollision(GameObject other)
    {	
    	if (other != transform.parent.gameObject){
	    	//Debug.Log("hit " + other.name);
	        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

	        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
	        int i = 0;

	        Meter meter = other.GetComponent<Meter>();
	        float hitAmt = partDmg;
	        if (transform.parent.parent.gameObject.GetComponent<Selection>().player == 1) hitAmt *= -1f;

	        while (i < numCollisionEvents)
	        {
	            if (rb)
	            {	

	                Vector3 pos = collisionEvents[i].intersection;
	                Vector3 force = collisionEvents[i].velocity * 5f * Mathf.Abs(meter.amt);
	                rb.AddForce(force);
	            }
	            if (meter){
	            	meter.amt += hitAmt;

	            	if (!other.GetComponent<Hurt>().doHurt) other.GetComponent<Hurt>().doHurt = true;
	            }
	            i++;
	        }
   		}
    }
}