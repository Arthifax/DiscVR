using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCController : MonoBehaviour
{

    [SerializeField] Rig rig;
    [SerializeField] float headTurnSpeed = 0.5f;
    [SerializeField] private bool turnsHead = false;
    float targetWeight = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (turnsHead)
        {
            TurnHead();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (turnsHead)
        {
            TurnHead();
        }
    }

    public void TurnHead()
    {
        StartCoroutine(SmoothLerp(headTurnSpeed));
    }

    private IEnumerator SmoothLerp(float time)
    {
        float elapsedTime = 0;

        if (targetWeight == 1)
        {
            targetWeight = 0;
            while (elapsedTime < time)
            {
                rig.weight = Mathf.Lerp(rig.weight, targetWeight, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            targetWeight = 1;
            while (elapsedTime < time)
            {
                rig.weight = Mathf.Lerp(rig.weight, targetWeight, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}

