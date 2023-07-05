using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript4P : MonoBehaviour {

    public string number;
    public bool tilted;
    Animation maskAnimation;
    AudioSource audiosource;

    public AudioClip headtilt;
    public AudioClip longtilt;

    private void Start()
    {
        maskAnimation = GetComponentInChildren<Animation>();
        audiosource = GetComponentInChildren<AudioSource>();
    }
    public void HitByPlayer()
    {
        if (!GetComponentInParent<StatueManager4P>().completed)
        {
            if (!maskAnimation.isPlaying)
            {
                if (!tilted)
                {
                    maskAnimation.Play("Tilt head");
                    audiosource.PlayOneShot(headtilt);
                    GetComponentInParent<StatueManager4P>().HitStatue(number);
                    tilted = true;
                }
            }
        }
    }

    public void TiltBack()
    {
        maskAnimation.Play("Wrong");
        audiosource.PlayOneShot(longtilt);
        tilted = false;
    }

    public void Happy()
    {
        maskAnimation.Play("Correct");
        audiosource.PlayOneShot(longtilt);
    }
}
