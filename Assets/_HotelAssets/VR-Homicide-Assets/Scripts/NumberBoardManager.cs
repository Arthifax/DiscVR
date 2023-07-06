using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NumberBoardManager : MonoBehaviour
{

    string currentSequence = "";
    string correctSequence = "7665";
    public Text currentDisplay;
    public Image passwordBar;


    public VideoPlayer videoPlayer;
    public GameObject lockScreen;
    public GameObject objectForVideo;

    public AudioClip keyPress;
    public AudioClip succesSfx;
    public AudioClip failureSfx;
    bool done;


    // update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TrySequence();
            Debug.Log("should play video");
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
                if (currentSequence == correctSequence)
                    TrySequence();
                else
                {
                    currentDisplay.GetComponent<AudioSource>().PlayOneShot(failureSfx);
                    currentSequence = "XXXX";
                }
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
        videoPlayer.enabled = true;
        videoPlayer.Prepare();
        currentDisplay.GetComponent<AudioSource>().PlayOneShot(succesSfx);
        if (PlayerPrefs.GetInt("progress") < 1)
            PlayerPrefs.SetInt("progress", 1);
        currentSequence = "";
        passwordBar.gameObject.SetActive(false);
        lockScreen.SetActive(false);
        objectForVideo.SetActive(true);
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
