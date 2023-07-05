using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childDelayTest : MonoBehaviour {

    
    public void Delayshit(int delay)
    {
        StartCoroutine(Delaycoroutine(delay));
        //Random.Range()
    }

    IEnumerator Delaycoroutine(int delay)
    {
        Debug.Log("ITEM: " + delay);
        yield return new WaitForSeconds(delay);
        Debug.Log("ITEM: " + delay + " after delay");

    }
}
