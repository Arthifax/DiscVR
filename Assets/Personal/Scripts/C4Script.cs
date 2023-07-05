using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C4Script : MonoBehaviour {

    private Vector3 startPos;

    private bool droppable = false;
    private bool correctDropPos = false;

    public BoxCollider boxcol;
    public TextMeshProUGUI countdownText;

    public GameObject boom;

    [SerializeField] private VaultScript vaultscript;

    [SerializeField] private AudioSource beep;

	void Start () {
        boxcol.enabled = false;
        countdownText.transform.parent.gameObject.SetActive(false);
        countdownText.text = "";
        startPos = transform.position;
    }

    void Update()
    {
        Collider col = boxcol;
        if (!col.enabled)
        {
            if (GetComponentInChildren<Renderer>() == null)
            {
                col.enabled = false;
                return;
            }

            if (GetComponentInChildren<Renderer>().enabled)
                col.enabled = true;
            else
                return;
        }
    }

   public void BombSet()
    {
        boxcol.enabled = false;
        droppable = false;

        if (isInCountdown)
            return;
        StartCoroutine(CountdownTimer());
    }

    private bool isInCountdown;

    IEnumerator CountdownTimer()
    {
        isInCountdown = true;

        for (int i = 0; i <= 5; i++)
        {
            yield return new WaitForSeconds(1);
            countdownText.text = (5 - i).ToString();
            if(i == 5)
            {
                BombExplode();
            }
            else
            {
                beep.Play();
            }
        }
    }

    void BombExplode()
    {
        if (correctDropPos)
        {
            vaultscript.Win();
            GameObject boomboom = Instantiate(boom, transform.position, Quaternion.identity);
            Destroy(boomboom, 2.8f);
        }
            
        else {
            StartCoroutine(ExplodeWaitAndRespawn());
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        countdownText.text = "";
    }

    IEnumerator ExplodeWaitAndRespawn()
    {
        GameObject boomboom = Instantiate(boom, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.8f); //Explosion animation time
        Destroy(boomboom);
        transform.position = startPos;
        boxcol.enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        countdownText.transform.parent.gameObject.SetActive(false);

        if(!UITextFunction.cannotSpawnBoemboems)
            isInCountdown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "vaultWithCollider")
            droppable = true;

        if(other.transform.name == "CorrectDropPos" && Vector3.Distance(transform.position, other.transform.position) < 0.5f)
        {
            correctDropPos = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "vaultWithCollider")
            droppable = false;

        if (other.transform.name == "CorrectDropPos" || Vector3.Distance(transform.position, other.transform.position) > 0.5f)
        {
            correctDropPos = false;
        }
    }
}
