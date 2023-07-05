using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class systemManager : MonoBehaviour {

  //  [SerializeField] GameObject[] screens;
    [SerializeField] GameObject[] screensToDelete;
    [SerializeField] Canvas[] texts;
    [SerializeField] Material blue;
    [SerializeField] Material red;
    [SerializeField] string correctSequence = "gbrrb";

    [SerializeField] GameObject[] videoPlayerScreens;
    [SerializeField] VideoPlayer[] videoPlayers;

    bool canAdd = true;
    [SerializeField] string input = "";
	
    void Start()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            player.Prepare();
        }
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartVideos();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            AddPress("r");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddPress("g");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddPress("b");
        }

    }

    public void AddPress(string color)
    {
        if (canAdd)
        {
            canAdd = false;
            input += color;
            CheckSequence();
            StartCoroutine(Timeout());
        }
    }

    void CheckSequence()
    {
        for(int i = 0; i < correctSequence.Length; i++)
        {
            try
            {
                if (input[i] == correctSequence[i] && input[i] != char.MinValue)
                {
                    if(input == correctSequence)
                    {
                        StartVideos();
                    }
                } else
                {
                    if(i != correctSequence.Length)
                    {
                        Reset();
                    }
                }
            }
            catch { 

            };
        }
    }

    void StartVideos()
    {
        foreach(GameObject c in screensToDelete)
        {
            c.gameObject.SetActive(false);
        }
        foreach (Canvas c in texts)
        {
            c.gameObject.SetActive(false);
        }

        //foreach (GameObject go in screens)
        //{
        //    if(go.name =="Screen")
        //    {
        //        go.SetActive(false);
        //    }
        //    else
        //    {
        //        go.transform.GetChild(0).gameObject.SetActive(false);
        //    }
        //}
        foreach(GameObject videoPlayerScreens in videoPlayerScreens)
        {
            videoPlayerScreens.SetActive(true);
        }
        foreach(VideoPlayer videoPlayer in videoPlayers)
        {
            videoPlayer.Play();
        }
    }

    void Reset()
    {
        input = "";

        foreach(GameObject go in screensToDelete)
        {
            if (go.name == "Screen 01 (1)" || go.name == "Screen 02 (1)" || go.name == "Screen 03 (1)")
            {
                go.GetComponent<Renderer>().material = red;
            }
            else
            {
                go.GetComponent<Renderer>().material = red;
            }
        }

        StartCoroutine(ResetToBlue());

    }

    IEnumerator ResetToBlue()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject go in screensToDelete)
        {
            if (go.name == "Screen 01 (1)" || go.name == "Screen 02 (1)" || go.name == "Screen 03 (1)")
            {
                go.GetComponent<Renderer>().material = blue;
            }
            else
            {
                go.GetComponent<Renderer>().material = blue; ;
            }
        }

        yield return null;
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(0.6f);
        canAdd = true;
    }
}
