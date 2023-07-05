using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {
    public Text texty;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KillMe()
    {
        //Destroy(gameObject);
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //        texty.text += "a";
    //    else
    //        texty.text += "b";
    //}

    //private void OnApplicationPause(bool pause)
    //{
    //    if(pause)
    //        texty.text += "c";//cbad wanneer uitvalt
    //    else
    //        texty.text += "d";
    //}

    //private void OnApplicationQuit()
    //{
    //    texty.text += "e";
    //}
}
