using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript : MonoBehaviour {

    public string number;
    public bool tilted;
    Animation maskAnimation;
    AudioSource audiosource;

    public AudioClip headtilt;
    public AudioClip longtilt;

    private void Start()
    {
        maskAnimation = GetComponent<Animation>();
        audiosource = GetComponent<AudioSource>();
    }
    public void HitByPlayer()
    {
        if (!GetComponentInParent<StatueManager>().completed)
        {
            if (!maskAnimation.isPlaying)
            {
                if (!tilted)
                {
                    if (gameObject.transform.rotation.eulerAngles.y == 90)
                    {
                        maskAnimation.Play("Tilt head 1");
                    }
                    else
                    {
                        maskAnimation.Play("Tilt head");
                    }
                    audiosource.PlayOneShot(headtilt);
                    GetComponentInParent<StatueManager>().HitStatue(number);
                    tilted = true;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            HitByPlayer();
        }
        if (Input.GetKeyDown(KeyCode.V)){
            TiltBack();
        }
    }

    public void TiltBack()
    {
        
        if(gameObject.transform.rotation.eulerAngles.y == 90)
        {
            maskAnimation.Play("Wrong 1");
        }
        else
        {
            maskAnimation.Play("Wrong");
        }
        audiosource.PlayOneShot(longtilt);
        tilted = false;
    }

    public void Happy()
    {
        if (gameObject.transform.rotation.eulerAngles.y == 90)
        {
            maskAnimation.Play("Correct 1");
        }
        else
        {
            maskAnimation.Play("Correct");
        }
        audiosource.PlayOneShot(longtilt);
    }
}
