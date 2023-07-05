using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public AudioClip[] clips;
    AudioSource sourcy;
	// Use this for initialization
	void Start () {
        sourcy = GetComponent<AudioSource>();
        PlayRandomClip();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitByPlayer()
    {
        PlayRandomClip();
    }

    void PlayRandomClip()
    {
        sourcy.clip = clips[Random.Range(0,clips.Length)];
        sourcy.Play();
    }
}
