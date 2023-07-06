using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    [SerializeField] private List<Animator> primaryAnimations;
    [SerializeField] private List<AudioSource> primaryAudio;
    [SerializeField] private int primaryAnimationdelaySeconds;
    [SerializeField] private int secondaryAnimationDelaySeconds;
    [SerializeField] private string primaryTrigger;
    [SerializeField] private List<GameObject> AnimationBoundObjects;
    private bool secondaryAnimationAllowed = false;

    private void Start()
    {
        if (primaryAnimations.Count > 0)
        {
            StartCoroutine(PrimaryAnimationsDelay());
        }
        if (secondaryAnimationDelaySeconds > 0)
        {
            StartCoroutine(SecondaryAnimationsDelay());
        }
    }

    private IEnumerator PrimaryAnimationsDelay()
    {
        yield return new WaitForSeconds(primaryAnimationdelaySeconds);
        foreach (Animator anim in primaryAnimations)
        {
            anim.SetTrigger(primaryTrigger);
        }
        if (primaryAudio.Count > 0)
        {
            foreach (AudioSource audio in primaryAudio)
            {
                audio.Play();
            }
        }
    }
    private IEnumerator SecondaryAnimationsDelay()
    {
        yield return new WaitForSeconds(secondaryAnimationDelaySeconds);
        secondaryAnimationAllowed = true;
        foreach(GameObject animationObject in AnimationBoundObjects)
        {
            animationObject.SetActive(true);
        }
    }
    public bool GetAnimationAllowed() { return secondaryAnimationAllowed; }
}
