using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffTrigger : MonoBehaviour
{
    public bool state;
    public List<GameObject> objectList;
    public bool wait = false;
    public void setState()
    {
        if (wait == false)
        {
            wait = true;
            state = !state;

            foreach (GameObject obj in objectList)
            {
                obj.SetActive(state);
            }
            StartCoroutine(statechange());
        }
    }

    IEnumerator statechange()
    {
        yield return new WaitForSeconds(0.5f);
        wait = false;
    }
}
