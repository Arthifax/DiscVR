using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("progress",0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("room select");
	}
}
