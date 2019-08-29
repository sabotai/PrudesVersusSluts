using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelColor : MonoBehaviour
{
	//public Color slutColor;
	//public Color prudeColor;
	public bool parentBased = true;
	public bool manualSlut = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	//if (transform.parent.parent.gameObject.GetComponent<Meter>().)

    	if (parentBased){
	        if (transform.parent.parent.parent.gameObject.GetComponent<Selection>().player == 1)
	        {
	        	GetComponent<Image>().color = Manager.slutColor;
	        }
	        else
	        {
	        	GetComponent<Image>().color = Manager.prudeColor;
	        }
        } else {
        	if (manualSlut) GetComponent<Image>().color = Manager.slutColor;
        	else GetComponent<Image>().color = Manager.prudeColor;
        }
    }
}
