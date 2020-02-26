using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityActivateBot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
    		Debug.Log("camBounds hit something");
    	if (col.transform.GetComponent<Bot>().enabled){
    		Debug.Log("bot activated");
            if (Manager.numPlayers == 1 && !Manager.usingBots){
        		col.gameObject.GetComponent<Move>().selected = true;
        		col.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().enabled = true;
            }
    	}
    }
}
