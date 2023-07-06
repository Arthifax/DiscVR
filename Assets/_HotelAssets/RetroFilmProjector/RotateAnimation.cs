using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] float speed = 10f;
    [SerializeField] Vector3 rotationAxis = Vector3.up;
    [SerializeField] bool turnedOn = false;
    [SerializeField] bool playsSound = false;

    // Update is called once per frame
    void Update()
    {
        if (turnedOn)
        {
            transform.Rotate(rotationAxis, speed * Time.deltaTime);
        }
    }

    public void ProjectorSwitch()
    {
        if (!turnedOn)
        {
            turnedOn = true;
            if (playsSound)
            {
                soundSource.Play();
            }

        }
        else
        {
            turnedOn = false;
            if (playsSound)
            {
                soundSource.Stop();
            }
        }
    }
}
