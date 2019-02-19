using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Randomizer : MonoBehaviour {

	public AudioClip[] titleClips;
	public AudioClip[] prudeWinClips, slutWinClips;
	public string[] prudeNames;
	public string[] slutNames;
	public static string prudeName = "Pruuds";
	public static string slutName = "Sl**ts";
	int selection = 0;
	bool won = false;


	// Use this for initialization
	void Start () {
		float randoF = Random.value;
		int rando = 0;
		if (randoF > 0.85f) rando = Random.Range(0, titleClips.Length);

		GetComponent<AudioSource>().clip = titleClips[rando];
		GetComponent<AudioSource>().Play();
		prudeName = prudeNames[rando];
		slutName = slutNames[rando];
		GetComponent<Text>().text = prudeName + " vs. " + slutName;
		selection = rando;
		won = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Manager.winner == slutName && !won) {
			GetComponent<AudioSource>().PlayOneShot(slutWinClips[selection]);
			won = true;
		}
		if (Manager.winner == prudeName && !won) {
			GetComponent<AudioSource>().PlayOneShot(prudeWinClips[selection]);
			won = true;
		}
		
	}
}
