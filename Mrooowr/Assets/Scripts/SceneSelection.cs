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
    public Image[] UIDots;
    string currentSelector = "";


    // Use this for initialization
    void Start () {
		man = Camera.main.gameObject;

        foreach(Image uidot in UIDots) {
            uidot.gameObject.SetActive(true);
            uidot.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < transform.childCount; i++){
            if (i != selection) UIDots[i].color = Manager.prudeUIColor;
            else UIDots[i].color = Manager.slutUIColor;
        }


        if (Time.time > textDelay)
        {
            subAnnouncer.transform.parent.gameObject.SetActive(true);
            subAnnouncer.text = "< Choose your battleground >";//"<◀ Choose your battleground ▶>";
        }

        //whoever presses button first controls which scene
        if (currentSelector == ""){
            if (Input.GetAxis("P1_Vertical") != 0f || Input.GetAxis("P1_Horizontal") != 0f) currentSelector = "P1";
            if (Input.GetAxis("P2_Vertical") != 0f || Input.GetAxis("P2_Horizontal") != 0f) currentSelector = "P2";
        } else {

        //DEBOUNCE STUFF
        float vAxis = Input.GetAxisRaw(currentSelector + "_Vertical");//"P1_Vertical") + Input.GetAxis("P2_Vertical");
        float hAxis = Input.GetAxisRaw(currentSelector + "_Horizontal");//_Horizontal") + Input.GetAxis("P2_Horizontal");
        float now = Time.realtimeSinceStartup;
        // check if user let go of the stick; if so, reset the input bounce control
        if (Mathf.Abs(hAxis) < 0.1f)
        {
            debounce = 0f; //nothing being pressed
        }

        // if it's been long enough since the last input, then we allow it
        if (now - debounce > repeat || debounce == 0f)
        {
            axisReady = true;

        }
        else
        {
            axisReady = false;
        }
        Debug.Log("vAxis = " + vAxis + " // hAxis = " + hAxis + " // axisReady = " + axisReady );
        //END DEBOUNCE

		if (axisReady)
        {
			if (Input.GetButtonDown(currentSelector+"_Action") || Input.GetButtonDown(currentSelector+"_Action")){
				prudes.SetActive(true);
				sluts.SetActive(true);
				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().confirmClip, 0.85f);
				//subAnnouncer.transform.parent.gameObject.SetActive(false);
                UIDots[0].transform.parent.gameObject.SetActive(false);
				this.enabled = false;

			}

			if (hAxis > 0f){//Input.GetAxis(currentSelector+"_Horizontal") > 0f){// || Input.GetAxis(currentSelector+"_Horizontal") > 0f ){
				if (selection < transform.childCount - 1){
					selection++;
				} else {
					selection = 0;
				}

				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);


                UpdateAvailable();
            }
			if (hAxis < 0f){//Input.GetAxis(currentSelector+"_Horizontal") < 0f){// || Input.GetAxis(currentSelector+"_Horizontal") < 0f ){
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
	}

    void UpdateAvailable()
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(selection).gameObject.SetActive(true);

        debounce = Time.realtimeSinceStartup; //ONLY RESET THE DEBOUNCE IF IT IS ACTUALLY BEING USED!!!
    }
}
