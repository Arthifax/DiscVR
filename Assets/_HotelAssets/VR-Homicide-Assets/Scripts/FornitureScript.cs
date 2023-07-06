using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FornitureScript : MonoBehaviour
{
    public bool closed = true;

    public AnimationClip[] clippers;

    public AudioClip openAndCloseSound;

    public void HitByPlayer()
    {
        if (!GetComponent<Animation>().isPlaying)
        {
            if (closed)
            {
                GetComponent<Animation>().Play(clippers[0].name);
                GetComponent<AudioSource>().PlayOneShot(openAndCloseSound);
                closed = false;
            }
            else
            {
                GetComponent<Animation>().Play(clippers[1].name);
                GetComponent<AudioSource>().PlayOneShot(openAndCloseSound);
                closed = true;
            }
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            HitByPlayer();
        }
    }
}
