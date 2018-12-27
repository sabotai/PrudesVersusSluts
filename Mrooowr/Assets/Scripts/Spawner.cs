using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject cloneMe;
	public float spawnRate = 5f;
	float nextSpawn;
	public bool randomize = true;
	public Vector3 spawnDir;
	// Use this for initialization
	void Start () {
		nextSpawn = spawnRate * Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + spawnRate * Random.value;
			GameObject clone = Instantiate (cloneMe, transform.position, Quaternion.Euler(spawnDir.x, spawnDir.y, spawnDir.z), transform) as GameObject;
			

		}



		
	}
}
