using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labels : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.gameObject.GetComponent<Move>().selected){
			GetComponent<TextMesh>().fontStyle = FontStyle.Bold;
		} else {
			GetComponent<TextMesh>().fontStyle = FontStyle.Normal;
		}

		GetComponent<TextMesh>().text = transform.parent.parent.gameObject.name;
		if (transform.parent.eulerAngles.y != 0f) {
			transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
		} else {

			transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
		}

		float amt = transform.parent.GetComponent<Meter>().amt;
		float max = transform.parent.GetComponent<Meter>().max;
		GetComponent<TextMesh>().color = Color.Lerp(Color.red, Color.white, (amt + max) / (max * 2f));
	}
}
