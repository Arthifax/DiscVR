using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueManager4P : MonoBehaviour {

    public Animation door;
    public StatueScript4P[] statues = new StatueScript4P[6];
    public bool completed;
    string sequence = "";

    public AudioClip correctSfx;
    public AudioClip failureSfx;

    public void HitStatue(string received)
    {
        sequence += received;
        if(sequence.Length == 6)
        {
            if(sequence == "143265")//ends in 5!
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
        foreach (StatueScript4P staty in statues)
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
        foreach (StatueScript4P staty in statues)
        {
            if (staty.tilted)
            {
                staty.TiltBack();
            }
        }
    }
}
