using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicCredit : MonoBehaviour {
	public string pretext = "";
	public string posttext = "";
	// Use this for initialization
	void Start () {
						GetComponent<Text>().text = pretext + GameObject.FindWithTag("Music").GetComponent<AudioSource>().clip.name + posttext;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 1; i < 3; i++){
			//if (GetComponent<Text>().text != "" && Input.GetAxis("P" + i + "_Horizontal") != 0f){
			if (Input.GetAxis("P" + i + "_Horizontal") != 0f){

				GetComponent<Text>().text = pretext + GameObject.FindWithTag("Music").GetComponent<AudioSource>().clip.name + posttext;
			}

		}
	}
}
