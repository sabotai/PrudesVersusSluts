using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroRandomizer : MonoBehaviour
{
	public GameObject followActive;
	public GameObject next;
	public GameObject alsoNext;

    // Start is called before the first frame update
    void Start()
    {
    	int random = Random.Range(0, transform.childCount);
    	transform.GetChild(random).gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!followActive.GetComponent<Image>().enabled){
        		//transform.parent.parent.gameObject.SetActive(false);
        		//next.SetActive(true);
        		alsoNext.SetActive(true);
        	} else {

        	}
    }
}
