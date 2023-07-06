using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasaCharacterBehaviour : MonoBehaviour
{
    private Animator characterBehaviour;
    [SerializeField] private StartAnimation startAnim;
    public bool canShootDown;
    public bool isHostage;
    private bool isFighting;
    private float timer;

    // Use this for initialization
    private void OnEnable()
    {
        timer = 5;
        characterBehaviour = GetComponent<Animator>();
        characterBehaviour.Play("Passive");
        StartCoroutine(StartAnim());
    }
	
	void Update ()
    {
        if(!isHostage)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                if(isFighting)
                {
                    timer = RNG.GimmeNumber(1f, 3f);
                    SwitchAnimationFight();
                }
                else
                {
                    timer = RNG.GimmeNumber(3f, 10f);
                    FireRandomIdle();
                }
            }
        }
	}

    private void SwitchAnimationFight()
    {
        int nextAnim = 0;
        if(canShootDown)
        {
            nextAnim = RNG.GimmeNumber(1, 2, true);
        }
        else
        {
            nextAnim = 1;
        }

        switch (nextAnim)
        {
            case 1:
                //characterBehaviour.Play("FiringAround", 0, UnityEngine.Random.Range(0f, 1f));
                characterBehaviour.SetTrigger("FiringAround");
                break;
            case 2:
                //characterBehaviour.Play("FiringDown", 0, UnityEngine.Random.Range(0f, 1f));
                characterBehaviour.SetTrigger("FiringDown");
                break;
            case 3:
                //characterBehaviour.Play("Passive", 0, UnityEngine.Random.Range(0f, 1f));
                characterBehaviour.SetTrigger("Passive");
                break;
        }
    }

    private void FireRandomIdle()
    {
        int nextAnim = 0;
        nextAnim = RNG.GimmeNumber(1, 2, true);
        switch (nextAnim)
        {
            case 1:
                characterBehaviour.SetTrigger("IdleFront1");
                break;
            case 2:
                characterBehaviour.SetTrigger("IdleFront2");
                break;
        }
    }

    public void StartFightScene()
    {
        isFighting = true;
        characterBehaviour.SetBool("Fighting", true);
    }

    public void StopFightScene()
    {
        isFighting = false;
        characterBehaviour.Play("Passive");
        characterBehaviour.SetBool("Fighting", false);
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(RNG.GimmeNumber(0f, 3f));
        switch (startAnim)
        {
            case StartAnimation.IdleFront0:
                //characterBehaviour.SetTrigger("IdleFront");
                characterBehaviour.Play("IdleFront0", 0, UnityEngine.Random.Range(0f, 1f));
                break;
            case StartAnimation.IdleDown:
                //characterBehaviour.SetTrigger("IdleDown");
                characterBehaviour.Play("IdleDown", 0, UnityEngine.Random.Range(0f, 1f));
                break;
            case StartAnimation.FiringAround:
                //characterBehaviour.SetTrigger("FiringAround");
                characterBehaviour.Play("FiringAround", 0, UnityEngine.Random.Range(0f, 1f));
                break;
            case StartAnimation.FiringDown:
                //characterBehaviour.SetTrigger("FiringDown");
                characterBehaviour.Play("FiringDown", 0, UnityEngine.Random.Range(0f, 1f));
                break;
            case StartAnimation.Passive:
                //characterBehaviour.SetTrigger("Passive");
                characterBehaviour.Play("Passive", 0, UnityEngine.Random.Range(0f, 1f));
                break;
            default:
                break;
        }
    }
}

public enum StartAnimation
{
    IdleFront0,
    IdleDown,
    FiringAround,
    FiringDown,
    Passive
}
