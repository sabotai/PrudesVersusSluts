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
			GetComponent<Text>().enabled = true;
			//transform.parent.parent.GetChild(0).gameObject.GetComponent<Outline>().effectDistance = new Vector2(5, -5);
		} else {
			GetComponent<Text>().fontStyle = FontStyle.Normal;
			GetComponent<Text>().enabled = false;
			//transform.parent.parent.GetChild(0).gameObject.GetComponent<Outline>().effectDistance = new Vector2(1, -1);
		}
		FixRot();
		GetComponent<Text>().text = transform.parent.parent.parent.gameObject.name;

		float amt = transform.parent.parent.parent.GetComponent<Meter>().amt;
		float max = transform.parent.parent.parent.GetComponent<Meter>().max;
		float pct = (amt + max) / (max * 2f);
		transform.parent.gameObject.GetComponent<Image>().fillAmount = pct;

		//transform.parent.gameObject.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, pct);
	}

	public void FixRot(){

		if (transform.parent.parent.parent.eulerAngles.y != 0f) {
			transform.parent.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
		} else {

			transform.parent.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
		}
	}
}
