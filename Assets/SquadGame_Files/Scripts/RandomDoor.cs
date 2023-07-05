using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDoor : MonoBehaviour
{
    [SerializeField]List<GameObject> gameobjects;
    bool busy = false;
    public bool temp = false;

    private void Update()
    {
        if (temp)
        {
            temp = false;
            GenerateRandom();
        }
    }
    public void GenerateRandom()
    {
        if (busy == false)
        {
            busy = true;
            StartCoroutine(waitseconds());
        }
    }

    IEnumerator waitseconds()
    {
        int random = Random.Range(0, gameobjects.Count);
        gameobjects[random].GetComponent<AnimationTrigger>().UpdateAnimation();
        yield return new WaitForSeconds(10);
        gameobjects[random].GetComponent<AnimationTrigger>().UpdateAnimation();
        yield return new WaitForSeconds(2);
        busy = false;
        StopCoroutine(waitseconds());

    }
}
