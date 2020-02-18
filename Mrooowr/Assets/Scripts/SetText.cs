using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
	public int player = 1;
	SelectCharacters sel;
	public bool useCharName = false;

    // Start is called before the first frame update
    void Start()
    {
        if (player == 1) GetComponent<Text>().text = Randomizer.prudeName; else  GetComponent<Text>().text = Randomizer.slutName;
        sel = transform.parent.parent.parent.GetComponent<SelectCharacters>();
    }

    // Update is called once per frame
    void Update()
    {
        if (useCharName) {
        	if (player == 1) GetComponent<Text>().text = sel.prefab[sel.currentSelection].GetComponent<AnimManager>().namePrude;
        	if (player == 2) GetComponent<Text>().text = sel.prefab[sel.currentSelection].GetComponent<AnimManager>().nameSlut;
    }
}
}
