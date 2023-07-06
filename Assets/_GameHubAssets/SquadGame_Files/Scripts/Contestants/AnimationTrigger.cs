using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private Animation myAnimation;
    [SerializeField] private AnimationClip[] myAnimations;
    private int index = 0;
    public bool play = false;

    void Start()
    {
        myAnimation = this.GetComponent<Animation>();
    }

    private void Update()
    {
        if (play)
        {
            UpdateAnimation();
            play = false;
        }
    }
    public void UpdateAnimation()
    {
        if (myAnimation.isPlaying == false)
        {
            if (index < myAnimations.Length)
            {
                myAnimation.clip = myAnimations[index];
                myAnimation.Play();
                index++;
            }
            else
            {
                index = 0;
                UpdateAnimation();
            }
        }
    
    
    }
}
