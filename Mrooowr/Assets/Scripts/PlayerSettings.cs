using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {
	int selection = 2;
	public Bot bot;
	public Transform botHolder;
	public GameObject p1Controls, p2Controls, pruuds, sloots;
	public SceneSelection sceneS;
	public GameObject sceneSUI;
	public Text subAnnouncer;
	GameObject man;
	public Color color1, color2;
	public GameObject botScene, quitDisplay;
	CamMove camMove;
    public GameObject leftArrow, rightArrow;
    public Image dotA, dotB, dotC, dotD;


    float debounce1 = 0f;
    float debounce2 = 0f;
    public float repeat = 0.1f;  // reduce to speed up auto-repeat input
    bool axisReady1 = true;
    bool axisReady2 = true;

    // Use this for initialization
    void Start () {
    	camMove = Camera.main.gameObject.transform.GetChild(0).gameObject.GetComponent<CamMove>();
		man = Camera.main.gameObject;
		GetComponent<Image>().color = color2;
		
        p1Controls.SetActive(true);
        //p2Controls.SetActive(true);

        dotA.enabled = true;
        dotB.enabled = true;
        dotC.enabled = true;
        dotD.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (selection == 0) {
            subAnnouncer.text = "Bonus Survival Mode >";
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            dotA.color = color1;
            dotB.color = color2;
            dotC.color = color2;
            dotD.color = color2;
            quitDisplay.SetActive(false);
            Manager.usingBots = false;
            Manager.numPlayers = 1;
        } else if (selection == 1) {
            if (Manager.debug) subAnnouncer.text = "< Bot Practice Match >";
            else subAnnouncer.text = "Bot Practice Match >"; 
            if (Manager.debug) leftArrow.SetActive(true); else leftArrow.SetActive(false); 
            rightArrow.SetActive(true);
            dotA.color = color2;
            dotB.color = color1;
            dotC.color = color2;
            dotD.color = color2;
            quitDisplay.SetActive(false);
            Manager.usingBots = true;
            Manager.numPlayers = 1;
        } else if (selection == 2) {
                subAnnouncer.text = "< 2-Player >";
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
                Manager.usingBots = false;
                dotA.color = color2;
                dotB.color = color2;
                dotC.color = color1;
                dotD.color = color2;
                quitDisplay.SetActive(false);
                Manager.numPlayers = 2;
        } else if (selection == 3) {
                    subAnnouncer.text = "< Quit game";
                    leftArrow.SetActive(true);
                    rightArrow.SetActive(false);
                    Manager.usingBots = false;
                    dotA.color = color2;
                    dotB.color = color2;
                    dotC.color = color2;
                    dotD.color = color1;
                    quitDisplay.SetActive(true);
                }





        if (Input.anyKeyDown)
        {
            if (Input.GetButtonDown("P1_Action") || Input.GetButtonDown("P2_Action"))
            {
                //p1Controls.SetActive(true);
                Manager.numPlayers = Mathf.Max(1, selection);
                if (selection == 2)
                {
                    sceneS.enabled = true;
                    sceneSUI.SetActive(true);
                    p2Controls.SetActive(true);
                }
                else if (selection == 1)     {
                   
                    sceneS.enabled = true;
                    sceneSUI.SetActive(true);
                    p2Controls.SetActive(false);
                } else if (selection == 3){
                    Application.Quit();
                } else if (selection == 0){

                     pruuds.SetActive(true);
                    sceneSUI.SetActive(true);
                    for (int i = 0; i < sloots.transform.childCount; i++)
                    {
                        Destroy(sloots.transform.GetChild(i).gameObject);//.gameObject.SetActive(false);
                                                                         //sloots.transform.DetachChildren();
                    }
                    int howMany = botHolder.childCount;
                    for (int i = 0; i < howMany; i++)
                    {
                        botHolder.GetChild(0).parent = sloots.transform;
                    }

                    dotA.transform.parent.gameObject.SetActive(false);

                    //enable scene selection?
                    //sceneS.enabled = true;
                    //sceneSUI.SetActive(true);
                    botScene.transform.parent = sceneS.transform;
                    botScene.SetActive(true);
                    sceneS.transform.GetChild(0).gameObject.SetActive(false);
                    botScene.transform.SetAsFirstSibling();

                    Manager.p2Ready = true;
                    sloots.SetActive(true);
                    //Manager.usingBots = true;
                    //pruuds.SetActive(true);

                    //camMove.enabled = true;
                }

                man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().confirmClip, 0.85f);
                gameObject.SetActive(false);
                //GetComponent<Image>().enabled = false;
                this.enabled = false;

            }
        }


        //float vAxis1 = Input.GetAxisRaw("P1_Vertical");
        //float hAxis1 = Input.GetAxisRaw("P1_Horizontal"); 
        float vAxis2 = Input.GetAxisRaw("P2_Vertical");
        float hAxis2 = Input.GetAxisRaw("P2_Horizontal");                                                   //bot
        float now = Time.realtimeSinceStartup;

        /*
        // check if user let go of the stick; if so, reset the input bounce control
        if (vAxis1 != 0f && hAxis1 != 0f)
        {
            debounce1 = 0f; //resets debounce
        }
        */
        if (vAxis2 != 0f && hAxis2 != 0f)
        {
            debounce2 = 0f; //resets debounce
        }

        /*
        // if it's been long enough since the last input, then we allow it
        if (now - debounce1 > repeat)
        {
            axisReady1 = true;
            debounce1 = Time.realtimeSinceStartup;
        }
        else
        {
            axisReady1 = false;
        }
		*/

        // if it's been long enough since the last input, then we allow it
        if (now - debounce2 > repeat || debounce2 == 0f)
        {
            axisReady2 = true;
            //debounce2 = Time.realtimeSinceStartup;
        }
        else
        {
            axisReady2 = false;
        }

        if (axisReady2) { 
        if (Input.GetAxisRaw("P2_Horizontal") > 0f){// || Input.GetAxis("P1_Horizontal") > 0f ){
                if (selection == 0){

                        selection = 1;
                        GetComponent<Image>().color = color1;
                    //bot.enabled = false;
                    p1Controls.SetActive(true);
                    p2Controls.SetActive(false);
                    //hide botscene and show multi scene
                    botScene.SetActive(false);
                    sceneS.transform.GetChild(0).gameObject.SetActive(true);

                        man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);
 

                } else if (selection == 1){
				    selection = 2;
				    GetComponent<Image>().color = color2;
				    //bot.enabled = false;
                    p1Controls.SetActive(true);
				    //p2Controls.SetActive(true);
				    //hide botscene and show multi scene
                    botScene.SetActive(false);
                    sceneS.transform.GetChild(0).gameObject.SetActive(true);
				    man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);
                } else if (selection == 2){
                    selection = 3;
                    GetComponent<Image>().color = color1;
                    //bot.enabled = false;
                    p2Controls.SetActive(false);
                    p1Controls.SetActive(false);
                    //hide botscene and show multi scene
                    botScene.SetActive(false);
                    sceneS.transform.GetChild(0).gameObject.SetActive(true);
                    man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);
                } else if (selection == 3){

                        man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().denyClip, 0.5f);
 
                }

				debounce2 = Time.realtimeSinceStartup;
			}
			if (Input.GetAxisRaw("P2_Horizontal") < 0f){//} || Input.GetAxis("P1_Horizontal") < 0f ){
                if (selection == 2){
        				selection = 1;
                    GetComponent<Image>().color = color1;
                    //bot.enabled = false;
                    p1Controls.SetActive(true);
                    p2Controls.SetActive(false);
                    //hide botscene and show multi scene
                    botScene.SetActive(false);
                    sceneS.transform.GetChild(0).gameObject.SetActive(true);

        				man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);
                } else if (selection == 3){
                    selection = 2;
                    GetComponent<Image>().color = color2;
                    //bot.enabled = false;
                    p1Controls.SetActive(true);
                   // p2Controls.SetActive(true);
                    //hide botscene and show multi scene
                    botScene.SetActive(false);
                    sceneS.transform.GetChild(0).gameObject.SetActive(true);

                    man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);
                } else if (selection == 1){
                    if (Manager.debug){
                        selection = 0;
                        //show botscene and hide multi scene
                        botScene.SetActive(true);
                        sceneS.transform.GetChild(0).gameObject.SetActive(false);

                        GetComponent<Image>().color = color2;

                        //bot.enabled = true;
                        p2Controls.SetActive(false);
                        //show botscene and hide multi scene
                        botScene.SetActive(true);
                        sceneS.transform.GetChild(0).gameObject.SetActive(false);

                        man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);


                    /*
                        GetComponent<Image>().color = color1;

                        //bot.enabled = true;
                        p2Controls.SetActive(false);
                        //show botscene and hide multi scene
                        //botScene.SetActive(true);
                        sceneS.transform.GetChild(0).gameObject.SetActive(false);
*/
                        man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().selectClip, 0.5f);
                    } else {
                        man.GetComponent<AudioSource>().PlayOneShot(man.GetComponent<Manager>().denyClip, 0.5f);
 
                    }
                }

				debounce2 = Time.realtimeSinceStartup;
			}


			//transform.GetChild(selection).gameObject.SetActive(true);
		}
	}
}
