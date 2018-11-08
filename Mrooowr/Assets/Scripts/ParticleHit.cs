using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour {

    ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float partDmg = 0f;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update(){
    	var col = part.collision;
    	if (transform.parent.gameObject.layer != col.collidesWith.value){
    		//Debug.Log("changing " + LayerMask.LayerToName(col.collidesWith.value) + " to " + LayerMask.LayerToName(transform.parent.gameObject.layer));
    		col.collidesWith = LayerMask.GetMask(LayerMask.LayerToName(transform.parent.gameObject.layer));
    	}
    }

    void OnParticleCollision(GameObject other)
    {	
    	if (other != transform.parent.gameObject){
	    	Debug.Log("hit " + other.name);
	        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

	        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
	        int i = 0;

	        Meter meter = other.GetComponent<Meter>();
	        float hitAmt = partDmg;
	        if (transform.parent.gameObject.GetComponent<Meter>().isSlut) hitAmt *= -1f;

	        while (i < numCollisionEvents)
	        {
	            if (rb)
	            {
	                Vector3 pos = collisionEvents[i].intersection;
	                Vector3 force = collisionEvents[i].velocity * 10;
	                rb.AddForce(force);
	            }
	            if (meter){
	            	meter.amt += hitAmt;
	            }
	            i++;
	        }
   		}
    }
}