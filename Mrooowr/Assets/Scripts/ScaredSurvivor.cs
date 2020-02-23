using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaredSurvivor : MonoBehaviour
{
	public Material regularText, scaredText;
	bool oneLeft;
    // Start is called before the first frame update
    void Start()
    {
        oneLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 1 && !oneLeft){
        	transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().material = scaredText;
        	oneLeft = true;
        } else if (transform.childCount > 1 && oneLeft){
        	oneLeft = false;
        	transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().material = regularText;

        }
    }
}
