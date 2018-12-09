using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public static bool gameOver = false;
	public static int howManyChars;
	public static bool p1Ready = false;
	public static bool p2Ready = false;
	public CamMove camMover;
	public AudioClip selectClip, confirmClip;
	public static string winner = " ";
	// Use this for initialization
	void Start () {
		p1Ready = false;
		p2Ready = false;
		gameOver = false;
		winner = " ";
	}
	
	// Update is called once per frame
	void Update () {
		if (p1Ready && p2Ready){
			camMover.enabled = true;
		}

		if (Input.GetButtonDown("Cancel")) SceneManager.LoadScene(0);

		if (gameOver && Input.GetButtonDown("Submit")) SceneManager.LoadScene(0);
	}
}
