using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

public class SwapMat : MonoBehaviour {


	public Material prudeMat, slutMat;
	Transform slutParent, prudeParent;
	// Use this for initialization
	void Start () {
		slutParent = GameObject.Find("Sluts").transform;
		prudeParent = GameObject.Find("Prudes").transform;
	}
	
	// Update is called once per frame
	void Update () {
		DoodleAnimator anim = GetComponent<DoodleAnimator>();
		if (transform.parent.parent == slutParent && GetComponent<Renderer>().material != slutMat) {
			//GetComponent<ParticleSystem>().
			GetComponent<Renderer>().material = slutMat;
			GetComponent<ParticleSystemRenderer>().material = slutMat;
			GetComponent<ParticleSystemRenderer>().trailMaterial = slutMat;
		}
		
		if (transform.parent.parent == prudeParent && GetComponent<Renderer>().material != prudeMat) {
			GetComponent<Renderer>().material = prudeMat;
			GetComponent<ParticleSystemRenderer>().material = prudeMat;
			GetComponent<ParticleSystemRenderer>().trailMaterial = prudeMat;
			

		}
		
	}
}
