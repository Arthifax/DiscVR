using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneWall : MonoBehaviour
{
    public Material normal;
    public Material lit;

    public GameObject[] phones;

    public float animationDuration;
    private float timer;

    private bool shouldCount;

    public AudioSource audio;

    public void StartCounting()
    {
        shouldCount = true;
    }

    private void Start()
    {
        timer = 5;
        phones[1].SetActive(false);
        phones[3].SetActive(false);
        phones[5].SetActive(false);
        phones[7].SetActive(false);
        phones[9].SetActive(false);
    }

    private void Update()
    {
        if(shouldCount)
        {
            if(timer < 0)
            {
                timer = animationDuration;
                StartCoroutine(DoTheThing());
            }
            timer -= Time.deltaTime;
        }
    }

    IEnumerator DoTheThing()
    {
        // Phone 1
        Debug.Log("Phone 1");
        phones[0].SetActive(false);
        phones[1].SetActive(true);
        audio.Play();
        yield return new WaitForSeconds(1f);
        phones[1].SetActive(false);
        phones[0].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        // Phone 2
        Debug.Log("Phone 2");
        phones[2].SetActive(false);
        phones[3].SetActive(true);
        audio.Play();
        yield return new WaitForSeconds(1f);
        phones[3].SetActive(false);
        phones[2].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        // Phone 3
        Debug.Log("Phone 3");
        phones[4].SetActive(false);
        phones[5].SetActive(true);
        audio.Play();
        yield return new WaitForSeconds(1f);
        phones[5].SetActive(false);
        phones[4].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        // Phone 4
        Debug.Log("Phone 4");
        phones[6].SetActive(false);
        phones[7].SetActive(true);
        audio.Play();
        yield return new WaitForSeconds(1f);
        phones[7].SetActive(false);
        phones[6].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        // Phone 5
        Debug.Log("Phone 5");
        phones[8].SetActive(false);
        phones[9].SetActive(true);
        audio.Play();
        yield return new WaitForSeconds(1f);
        phones[9].SetActive(false);
        phones[8].SetActive(true);

        //WaitForSeconds wfs1 = new WaitForSeconds(1),
        //    wfs2 = new WaitForSeconds(.1f);

        //for (int i = 0; i < phones.Length - 1; i++)
        //{
        //    phones[i].SetActive(false);
        //    phones[i + 1].SetActive(true);
        //    audio.Play();
        //    yield return wfs1;
        //    phones[i + 1].SetActive(false);
        //    phones[i].SetActive(true);
        //    yield return wfs2;
        //}
    }
}
