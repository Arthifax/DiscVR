using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueManager : MonoBehaviour {

    public Animation door;
    public StatueScript[] statues = new StatueScript[6];
    public bool completed;
    string sequence = "";
    [SerializeField] string correctSequence = "143265";

    public AudioClip correctSfx;
    public AudioClip failureSfx;

    public void HitStatue(string received)
    {
        sequence += received;
        if(sequence.Length == 7)
        {
            if(sequence == correctSequence)
            {
                StartCoroutine(Correct());
            }
            else
            {
                StartCoroutine(SetBackSoon());
            }
        }
        //string attempt = "";
        //for(int i = 0; i < 6; i++)
        //{
        //    if (statues[i].tilted)
        //        attempt += "1";
        //    else
        //        attempt += "0";
        //}
        //if (attempt == "000001")//correct sequence
        //    Correct();

    }

    IEnumerator Correct()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<AudioSource>().PlayOneShot(correctSfx);
        completed = true;
        foreach (StatueScript staty in statues)
        {
            if (staty.tilted)
            {
                staty.Happy();
            }
        }
        door.Play();
    }

    IEnumerator SetBackSoon()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<AudioSource>().PlayOneShot(failureSfx);
        sequence = "";
        foreach (StatueScript staty in statues)
        {
            if (staty.tilted)
            {
                staty.TiltBack();
            }
        }
    }
}
