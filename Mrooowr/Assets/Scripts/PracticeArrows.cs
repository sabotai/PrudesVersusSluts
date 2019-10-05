using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeArrows : MonoBehaviour
{
	public Transform slutParent, prudeParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (Manager.p1Ready && Manager.p2Ready){
        Vector3 slutAvg = Vector3.zero;
		for (int i = 0; i < slutParent.childCount; i++){
			slutAvg += slutParent.GetChild(i).position;
		}
		slutAvg /= slutParent.childCount;

		Vector3 prudeAvg = Vector3.zero;
			for (int i = 0; i < prudeParent.childCount; i++){
				prudeAvg += prudeParent.GetChild(i).position;
			}
		prudeAvg /= prudeParent.childCount;

		if (prudeAvg.x < slutAvg.x){
			transform.GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).gameObject.SetActive(false);

		}
    	else 	{
			transform.GetChild(0).gameObject.SetActive(true);
			transform.GetChild(1).gameObject.SetActive(false);
    	}
		}
	}
		
}
