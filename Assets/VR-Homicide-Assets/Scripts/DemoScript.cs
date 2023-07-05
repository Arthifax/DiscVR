using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour {
    
    public AudioClip lockedSfx;

    public void HitByPlayer()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
                GetComponent<AudioSource>().PlayOneShot(lockedSfx);
        }
    }
}
