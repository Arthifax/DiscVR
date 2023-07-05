using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("progress") < 2)
            PlayerPrefs.SetInt("progress", 2);
    }
}
