using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelection : MonoBehaviour {

	int selection = 0;
	public GameObject prudes, sluts;
	public Text subAnnouncer;
	public float textDelay = 2f;
	GameObject man;

    float debounce = 0f;
    public float repeat = 0.1f;  // reduce to speed up auto-repeat input
    bool axisReady = true;

    // Use this for initialization
    void Start () {
		man = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {


        if (Time.time > textDelay)
        {
            subAnnouncer.transform.parent.gameObject.SetActive(true);
            subAnnouncer.text = "< Choose your battleground >";//"<◀ Choose your battleground ▶>";
        }

        //DEBOUNCE STUFF

        float vAxis = Input.GetAxis("P1_Vertical") + Input.GetAxis("P2_Vertical");
        float hAxis = Input.GetAxis("P1_Horizontal") + Input.GetAxis("P2_Horizontal");
        float now = Time.realtimeSinceStartup;
        // check if user let go of the stick; if so, reset the input bounce control
        if (Mathf.Abs(vAxis) < 0.1f && Mathf.Abs(hAxis) < 0.1f)
        {
            debounce = 0f;
        }

        // if it's been long enough since the last input, then we allow it
        if (now - debounce > repeat)
        {
            axisReady = true;

            debounce = Time.realtimeSinceStartup;
        }
        else
        {
            axisReady = false;
        }
        //END DEBOUNCE

		if (axisReady)
        {
			if (Input.GetButtonDown("P1_Action") || Input.GetButtonDown("P2_Action")){
				prudes.SetActive(true);
				sluts.SetActive(true);
				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().confirmClip, 0.85f);
				//subAnnouncer.transform.parent.gameObject.SetActive(false);
				this.enabled = false;

			}

			if (Input.GetAxis("P1_Horizontal") > 0f || Input.GetAxis("P2_Horizontal") > 0f ){
				if (selection < transform.childCount - 1){
					selection++;
				} else {
					selection = 0;
				}

				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);


                UpdateAvailable();
            }
			if (Input.GetAxis("P1_Horizontal") < 0f || Input.GetAxis("P2_Horizontal") < 0f ){
				if (selection > 0){
					selection--;
				} else {
					selection = transform.childCount - 1;
				}
				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);

                UpdateAvailable();

			}
		}
	}

    void UpdateAvailable()
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(selection).gameObject.SetActive(true);
    }
}
