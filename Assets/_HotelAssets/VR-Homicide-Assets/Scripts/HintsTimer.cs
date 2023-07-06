using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//1. = 4,5 -- 1.2 = 7,5 -- 1.3 = 10,5
public class HintsTimer : MonoBehaviour {

    public Text[] hints;
    public Image arrowPointer;
    private Animator animator;
    public AudioSource hintPopUp;

    public List<float> timer = new List<float>();
    private int waitASecID;

    void Start()
    {
        waitASecID = 0;
        animator = arrowPointer.GetComponent<Animator>();
        timer.Add(270f); //270
        timer.Add(180f); //450
        timer.Add(180f); //630

        StartCoroutine(HintTimer());
    }

    IEnumerator HintTimer()
    {
        yield return new WaitForSeconds(timer[waitASecID]);
        
        ShowHint();
        waitASecID++;
        if (waitASecID < hints.Length)
        {
            StartCoroutine(HintTimer());
        }
    }

    void ShowHint()
    {
        if (waitASecID > 0 && hints[waitASecID -1].gameObject.activeInHierarchy != false)
        {
            hints[waitASecID - 1].gameObject.SetActive(false);
        }
        hints[waitASecID].gameObject.SetActive(true);
        animator.SetTrigger("Activate");
        if (hintPopUp != null)
        {
            hintPopUp.Play();
        }
        print("waitASec " + waitASecID + " | Na " + timer[waitASecID] + " seconden.");
    }

}
