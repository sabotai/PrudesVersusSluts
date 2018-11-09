using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainPos : MonoBehaviour {
	public int minX, maxX, minY, maxY;
	Vector3 origPos;
	public float catchSpeed = 0.03f;
	// Use this for initialization
	void Start () {
		origPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x >= maxX || transform.position.x <= minX || transform.position.y <= minY || transform.position.y >= maxY){
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
		} else {
			transform.localPosition = Vector3.Lerp(transform.localPosition, origPos, catchSpeed);
		}

		/* new method test

				if (transform.position.x >= maxX){
			transform.position = Vector3.Lerp(transform.position, new Vector3(maxX, transform.position.y, transform.position.z), catchSpeed);
		} 
		if (transform.position.x <= minX) {
			transform.position = Vector3.Lerp(transform.position, new Vector3(minX, transform.position.y, transform.position.z), catchSpeed);

		}

		if (transform.position.y <= minY){

			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.y, minY, transform.position.z), catchSpeed);
		}

		if (transform.position.y >= maxY){
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.y, maxY, transform.position.z), catchSpeed);

		}
		*/
	}
}
