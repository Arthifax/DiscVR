using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontManMovement : MonoBehaviour
{
    private float time;
    [SerializeField] private Transform[] FrontManLerpPoints;
    [SerializeField] private float timeToReachTargetPosition = 10;
    [SerializeField] private GameObject EndGameUI;
    [SerializeField] private AudioSource footStepSound;
    void Update()
    {
        if (transform.position != FrontManLerpPoints[1].position)
        {
            time += Time.deltaTime / timeToReachTargetPosition;
            transform.position = Vector3.Lerp(FrontManLerpPoints[0].position, FrontManLerpPoints[1].position, time);
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Idle");
            footStepSound.Stop();
            footStepSound.enabled = false;
            EndGameUI.SetActive(true);
            this.enabled = false;
        }
    }
}
