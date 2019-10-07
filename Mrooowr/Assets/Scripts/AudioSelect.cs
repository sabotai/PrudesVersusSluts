using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelect : MonoBehaviour
{
	public AudioClip slutSelect, prudeSelect;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Meter>().isSlut) GetComponent<AudioSource>().clip = slutSelect;
        else GetComponent<AudioSource>().clip = prudeSelect;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Meter>().isSlut) GetComponent<AudioSource>().clip = slutSelect;
        else GetComponent<AudioSource>().clip = prudeSelect;
    }
}
