using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Labels : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.parent.parent.gameObject.GetComponent<Move>().selected){
			GetComponent<Text>().fontStyle = FontStyle.Bold;
			transform.parent.gameObject.GetComponent<Outline>().effectDistance = new Vector2(5, -5);
		} else {
			GetComponent<Text>().fontStyle = FontStyle.Normal;
			transform.parent.gameObject.GetComponent<Outline>().effectDistance = new Vector2(1, -1);
		}

		GetComponent<Text>().text = transform.parent.parent.parent.gameObject.name;
		if (transform.parent.parent.parent.eulerAngles.y != 0f) {
			transform.parent.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
		} else {

			transform.parent.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
		}

		float amt = transform.parent.parent.parent.GetComponent<Meter>().amt;
		float max = transform.parent.parent.parent.GetComponent<Meter>().max;
		transform.parent.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, (amt + max) / (max * 2f));
	}
}
