using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
	
	public float timerAmt;
	float startTime = 0f;
	public float pctComplete = 0f;
    // Start is called before the first frame update
    void Start()
    {
       startTime = Time.time;
       pctComplete = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        pctComplete = ((Time.time - startTime) / timerAmt);
		transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = pctComplete;

		if (pctComplete >= 1f) Destroy(gameObject);
		FixRot();
    }

    public void start(){

       startTime = Time.time;
       pctComplete = 0f;
    }	
    public void FixRot(){

		if (transform.parent.parent.eulerAngles.y != 0f) {
			transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
		} else {

			transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
		}
	}

}
