using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityThresh : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag("Bounds") && col.gameObject.GetComponent<Rigidbody2D>()){
			col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 15f;
			Debug.Log("turn on gravity for " + col.name);
		}
	}
	void OnTriggerExit2D(Collider2D col){
		Debug.Log("gravity exit");
		if (col.CompareTag("Bounds") && col.gameObject.GetComponent<Rigidbody2D>())
			col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
	}
}
