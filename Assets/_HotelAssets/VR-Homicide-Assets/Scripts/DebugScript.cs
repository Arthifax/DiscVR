using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {

    int ticks = 0;
    bool canGoNext = true;
	// Use this for initialization
	public void HitByPlayer()
    {
        ticks++;
        if(ticks > 10 && canGoNext)
        {
            PlayerPrefs.SetInt("progress", 3);
            canGoNext = false;
            Camera.main.GetComponent<NextLevelScript>().NextLevel("room select");
        }
    }
}
