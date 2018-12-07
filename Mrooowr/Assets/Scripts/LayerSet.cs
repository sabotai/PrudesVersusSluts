using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
/*
	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag("Quint")){
			Debug.Log("moving " + gameObject.name + " to " + LayerMask.LayerToName(col.gameObject.layer));
			gameObject.layer = col.gameObject.layer;
			GetComponent<SpriteRenderer>().sortingLayerName = LayerMask.LayerToName(col.gameObject.layer);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag("Quint")){
			Debug.Log("moving " + gameObject.name + " to " + LayerMask.LayerToName(col.gameObject.layer));
			gameObject.layer = col.gameObject.layer;
			GetComponent<SpriteRenderer>().sortingLayerName = LayerMask.LayerToName(col.gameObject.layer);
		}
	}
	*/
	void OnTriggerStay2D(Collider2D col){
		//Debug.Log(gameObject.name + " triggered");
		if (col.CompareTag("Quint")){
			//Debug.Log("moving " + gameObject.name + " to " + col.gameObject.name);
			transform.parent.gameObject.layer = LayerMask.NameToLayer(col.gameObject.name);
			transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = col.gameObject.name;
		}
	}
}
