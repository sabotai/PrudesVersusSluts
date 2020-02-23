using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeObject : MonoBehaviour
{
	Vector3 initPos;
	float amt = 0.8f;
	float amt2 = 0.7f;
	float speed = 56f;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent.parent.parent.parent.childCount == 1){
        	transform.localPosition = initPos + new Vector3(Mathf.Sin(Time.time * speed) * amt, Mathf.Sin(Time.time * speed) * amt2, 0f);
        	GetComponent<Text>().color = Color.Lerp(Color.white, new Color(1f, 0.75f, 0.016f, 1f), Mathf.PingPong(Time.time * 1.5f, 1));
        	} else {
        		transform.localPosition = initPos;
        		        	GetComponent<Text>().color = Color.white;
        	}
    }
}
