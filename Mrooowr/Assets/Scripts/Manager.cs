using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public static bool gameOver = false;
	public static int howManyChars;
	public static bool p1Ready = false;
	public static bool p2Ready = false;
	public static int numPlayers = 2;
	public CamMove camMover;
	public AudioClip selectClip, confirmClip;
	public static string winner = " ";
	public static Color prudeColor;
	public static Color slutColor;
	public Color prudeColorPub;
	public Color slutColorPub;
	public static bool usingBots = false;
	public bool muteMusic = false;
	bool mutedMusic = false;
	public AudioSource[] musicPlayers;
	public static bool prudeMode = false;
	public Transform slootLevel;

	// Use this for initialization
	void Start () {
		p1Ready = false;
		p2Ready = false;
		gameOver = false;
		winner = " ";
		usingBots = false;
	}
	
	// Update is called once per frame
	void Update () {
		prudeColor = prudeColorPub;
		slutColor = slutColorPub;

		Debug.Log("p1ready = " + p1Ready + "; p2ready = " + p2Ready);
		if (p1Ready && p2Ready){
			camMover.enabled = true;
		}

		if (Input.GetButtonDown("Cancel")) SceneManager.LoadScene(0);

		if (gameOver && Input.GetButtonDown("Submit")) SceneManager.LoadScene(0);


		if (Input.GetKeyDown(KeyCode.F1)) {
			 SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown(KeyCode.F2)) {
			 SceneManager.LoadScene(1);
		}
		//prude mode
		if (Input.GetKeyDown(KeyCode.F12)) {
			prudeMode = !prudeMode;

			if (prudeMode) slootLevel.parent = null;
			else slootLevel.parent = GameObject.Find("Scenes").transform;
		}
		//music muting
		if (Input.GetKeyDown(KeyCode.M)) muteMusic = !muteMusic;
		if (muteMusic && !mutedMusic){

			foreach (AudioSource musicPlayer in musicPlayers)
	        {
	            musicPlayer.enabled = false;
	        }
	        mutedMusic = true;
		} else if (!muteMusic && mutedMusic) {

			foreach (AudioSource musicPlayer in musicPlayers)
	        {
	            musicPlayer.enabled = true;
	        }
	        mutedMusic = false;
		}
	}
}
