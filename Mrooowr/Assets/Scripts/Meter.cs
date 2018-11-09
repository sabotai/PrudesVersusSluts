using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {

	public bool isSlut = true;
	public float amt;
	public float max = 1f;

	public Transform slutParent, prudeParent;
	// Use this for initialization
	void Start () {
        if (isSlut)
        {
            amt = -1f * max;
        }
        else
        {
            amt = 1f * max;
            GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (amt > 0 && isSlut) {
			isSlut = false; 

			transform.parent = prudeParent;
			GetComponent<Action>().SetAnim("prude");
			GetComponent<Move>().SetAnim("prude");

            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            if (GetComponent<Move>().selected){
				//Debug.Log("PRUDES WIN!");
				GetComponent<Move>().selected = false;
				slutParent.gameObject.GetComponent<Selection>().CycleSelection();
				//transform.parent.gameObject.GetComponent<Selection>().Select();
			}

		}else if (amt < 0 && !isSlut) {

			transform.parent = slutParent;
			isSlut = true;
			GetComponent<Action>().SetAnim("slut");
			GetComponent<Move>().SetAnim("slut");
            GetComponent<SpriteRenderer>().color = Color.white;
            if (GetComponent<Move>().selected){
				//Debug.Log("SLUTS WIN!");
				GetComponent<Move>().selected = false;

				prudeParent.gameObject.GetComponent<Selection>().CycleSelection();
			}

		}

		amt = Mathf.Clamp(amt, -max, max);
	}
}
