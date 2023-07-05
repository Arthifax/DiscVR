using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayNametagManager : MonoBehaviour {

	public HallwayNametag fourOTwo, fourOThree;
	bool completed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(fourOTwo.correctAnswer && fourOThree.correctAnswer && !completed)
		{
			completed = true;
			GetComponent<AudioSource>().Play();
		}
	}
}
