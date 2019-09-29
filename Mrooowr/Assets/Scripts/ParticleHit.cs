using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour {

    ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float partDmg = 0f;
    //public AudioClip emitClip, emitClipPrude, emitClipSlut;
    public bool limitLayers = true;
    public GameObject bloodPrefab;
    int maxBloodParts = 1;
    public float bloodSize= 1f;
    public bool friendlyBlood = false;

   // AudioSource aud;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        //aud = GetComponent<AudioSource>();
    }

    void Update(){
        /*
        if (transform.parent.parent.GetComponent<Selection>().player == 1) emitClip = emitClipSlut;
        else  emitClip = emitClipPrude;


    	if (part.particleCount > 0 && part.isEmitting) {
    		//ActionSounds();
    	} else {
    		aud.loop = false;
    		//Stop();
    	}
        */

        if (limitLayers){
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
   	
    }
    /*
    void ActionSounds(){
		if (!aud.isPlaying){
			aud.clip = emitClip;
			aud.loop = true;
			aud.Play();
			//aud.PlayDelayed(0.1f);//PlayOneShot(actionClip, 0.1f);
		}	
    }
    */

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
                    if (i < maxBloodParts) {
                        if (friendlyBlood || other.transform.parent != transform.parent.parent){
                            GameObject bloody = Instantiate(bloodPrefab, pos, Quaternion.identity) as GameObject;
                            var main = bloody.GetComponent<ParticleSystem>().main;
                            main.startSize = bloodSize;

                            if (other.name == "Horsey" || other.name == "Uni" || other.name == "Skully" || other.name == "Catgut" || other.name == "Birdy" || other.name == "Kitty") {
                                if (!Manager.prudeMode) main.startColor = Color.red; 
                            }
                            else if (other.name == "Eggy") main.startColor = new Color(255f/255f, 247f/255f, 50f/255f);
                            else if (other.name == "Jimmy") main.startColor = new Color(230f/255f, 96f/255f, 255f/255f);
                            else if (other.name == "Mango") main.startColor = new Color(239f/255f, 135f/255f, 81f/255f);
                            else if (other.name == "Fidgety") main.startColor = new Color(128f/255f, 128f/255f, 128f/255f);
                            else if (other.name == "Dumply") main.startColor = new Color(177f/255f, 143f/255f, 98f/255f);
                            else if (other.name == "Stormy") main.startColor = new Color(32f/255f, 139f/255f, 252f/255f);
                            else if (other.name == "Sunny") main.startColor = new Color(162f/255f, 251f/255f, 252f/255f);

                        } 
                    }
	            }
	            if (meter){
	            	meter.amt += hitAmt;
                    if (partDmg > .01f) other.GetComponent<Hurt>().hurtAmt = Random.Range(0.8f, 0.9f);
                    else if (partDmg < .01f) other.GetComponent<Hurt>().hurtAmt = Random.Range(1.1f, 1.2f);
                    else other.GetComponent<Hurt>().hurtAmt = Random.Range(0.95f, 1.05f);

                    if (!other.GetComponent<Hurt>().goodHurt && other.transform.parent == transform.parent.parent)  other.GetComponent<Hurt>().goodHurt = true;
	            	else if (!other.GetComponent<Hurt>().doHurt) other.GetComponent<Hurt>().doHurt = true;
	            }
	            i++;
	        }
   		}
    }
}