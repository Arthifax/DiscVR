using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeObject : MonoBehaviour
{
    public GameObject Camera;
    public Color EndColor;
    private Color startColor;

    private Color colorA, colorB;
    [Space]
    public int duration = 1;

    private float t = 0;
    public UnityEvent myEvent;

    public bool test = false;
    public bool canUpdate = false;


    private void Start()
    {
        startColor = this.GetComponent<Renderer>().material.color;
        this.transform.parent = Camera.transform;
        this.transform.position = Camera.transform.position;
    }
    private void Update()
    {
        if (canUpdate)
        {
            canUpdate = false;
            this.transform.localPosition = new Vector3(0f, 0, 0.2f);
            this.transform.rotation = Camera.transform.rotation;
            if (test)
            {
                test = false;
                StartFade();
            }
            if (t < 1)
            {
                canUpdate = true;
                this.GetComponent<Renderer>().material.color = Color.Lerp(colorA, colorB, t);
                t += Time.deltaTime / duration;
            }
        }
    }

    public void StartFade()
    {
        canUpdate = true;
        StartCoroutine(fadeTime());
    }
    IEnumerator fadeTime()
    {
        
        colorA = startColor;
        colorB = EndColor;
        t = 0;
        canUpdate = true;
        yield return new WaitForSeconds(duration);
        myEvent.Invoke();
        colorB = startColor;
        colorA = EndColor;
        t = 0;
        canUpdate = true;
    }
}
