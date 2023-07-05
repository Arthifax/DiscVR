﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManager4P : MonoBehaviour {

    public TicketScript4P currentTicket;
    public AudioClip getTicketSfx;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("progress") < 3)
            PlayerPrefs.SetInt("progress", 3);
    }

    public void playAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(getTicketSfx);
    }
}
