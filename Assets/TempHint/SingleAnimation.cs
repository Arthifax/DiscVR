using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animation myAnimation;
    [SerializeField] private AnimationClip myClip;

    public void playAnimation()
    {
        if (!myAnimation.isPlaying)
        {
            myAnimation.clip = myClip;
            myAnimation.Play();
        }
    }
}
