using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NumberBoardManager4P : MonoBehaviour
{
    [SerializeField] bool trySequance = false;
    [SerializeField] string currentSequence = "";
    public Text currentDisplay;

    public GameObject videoPlayerObject;
    public VideoPlayer videoPlayer;

    public AudioClip keyPress;
    public AudioClip succesSfx;
    public AudioClip failureSfx;
    bool done;

    private void Update()
    {
        if (trySequance)
        {
            TrySequence();
            trySequance = false;
        }
    }

    public void AddThis(string received)
    {
        if (!done)
        {
            if (received.Length < 2 && currentSequence.Length < 4)
            {
                currentSequence += received;
                currentDisplay.GetComponent<AudioSource>().PlayOneShot(keyPress);
            }
            else if (received == "DONE")
            {
                if (currentSequence == "3458")
                    TrySequence();
                else
                    currentDisplay.GetComponent<AudioSource>().PlayOneShot(failureSfx);
            }
            else if (received == "BACK")
            {
                currentSequence = currentSequence.Remove(currentSequence.Length - 1);
                currentDisplay.GetComponent<AudioSource>().PlayOneShot(keyPress);
            }
            else
            {
                currentDisplay.GetComponent<AudioSource>().PlayOneShot(keyPress);
            }
            currentDisplay.text = currentSequence;
        }
    }

    void TrySequence()
    {
        currentDisplay.GetComponent<AudioSource>().PlayOneShot(succesSfx);
        if (PlayerPrefs.GetInt("progress") < 1)
            PlayerPrefs.SetInt("progress", 1);
        currentSequence = "";
        videoPlayerObject.SetActive(true);
        videoPlayer.enabled = true;
        videoPlayer.Play();
        done = true;
    }

    //private void OnApplicationPause(bool pause)
    //{
    //    if (pause && done)
    //        videoPlayer.Stop();//cbad wanneer uitvalt
    //    else if(!pause && done)
    //        StartCoroutine(StartSoon());
    //}

    //IEnumerator StartSoon()
    //{
    //    yield return new WaitForSeconds(1);
    //    videoPlayer.Play();
    //}
}
