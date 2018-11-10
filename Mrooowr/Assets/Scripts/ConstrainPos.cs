using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainPos : MonoBehaviour {
	public float minX, maxX, minY, maxY;
	Vector3 origPos;
	public float catchSpeed = 0.03f;
	public Vector3 origV3;
	// Use this for initialization
	void Start () {
		origPos = transform.localPosition;
		minX = -16f;
		maxX = 16f;
		minY = -9f;
		maxY = 9f;
		catchSpeed = 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
		float newMaxX = maxX + Camera.main.transform.position.x;
		float newMaxY = maxY + Camera.main.transform.position.y;
		float newMinX = minX + Camera.main.transform.position.x;
		float newMinY = minY + Camera.main.transform.position.y;
		origV3 = transform.parent.TransformPoint(origPos); //must convert to world space using parent!!
		//Debug.Log("origV3 = " + origV3);
		
		transform.position = new Vector3(Mathf.Clamp(origV3.x, newMinX, newMaxX), Mathf.Clamp(origV3.y, newMinY, newMaxY), origV3.z);

	}
}
