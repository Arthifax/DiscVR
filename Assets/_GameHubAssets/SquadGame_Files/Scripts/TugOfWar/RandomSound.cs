using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public AudioSource mySource;
    public AudioClip originalClip;
    public AudioClip funnyHAHAClip;
    [Space]
    [Range(0.0f,100.0f)]
    public int occurence;
    [Space]
    public bool play = false;

    private void Update()
    {
        if (play)
        {
            play = false;
            playAudio();
        }
    }
    void playAudio()
    {
        //if (mySource.isPlaying == false)
        //{
            if (Random.Range(0, 100) <= occurence)
            {
                mySource.clip = funnyHAHAClip;
                mySource.Play();
            }
            else
            {
                mySource.clip = originalClip;
                mySource.Play();
            }
        //}

    }
}
