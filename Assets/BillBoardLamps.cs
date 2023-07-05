using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BillBoardLamps : MonoBehaviour
{
    private GameObject lampParent;
    public float waitTime = 1;
    public int snakeLength = 10;
    public Color onColor;
    public Color offColor;

    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        lampParent = this.GameObject();
        
        for (int i = 0; i < lampParent.transform.childCount; i++)
        {
            GameObject current;
            current = lampParent.transform.GetChild(i).gameObject;
            current.GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);
        }

        StartCoroutine(lampies());
    }

    // Update is called once per frame
    void Update()
    {



    }

    IEnumerator lampies()
    {
        
        if (index < lampParent.transform.childCount)
        {
            GameObject current;
            current = lampParent.transform.GetChild(index).gameObject;
            if (index -snakeLength >= 0)
            {
                
                current = lampParent.transform.GetChild(index - snakeLength).gameObject;
                current.GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);

            }
            else if (index < snakeLength)
            {
                current = lampParent.transform.GetChild(index +  lampParent.transform.childCount - snakeLength).gameObject;
                current.GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor); 
            }
            if (index -snakeLength + lampParent.transform.childCount/2 >= 0)
            {
                
                current = lampParent.transform.GetChild(index - snakeLength + lampParent.transform.childCount/2).gameObject;
                current.GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);

            }
            else if (index < snakeLength)
            {
                current = lampParent.transform.GetChild(index + lampParent.transform.childCount - snakeLength).gameObject;
                current.GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor); 
            }
            

            current = lampParent.transform.GetChild(index).gameObject;
            current.GetComponent<Renderer>().material.SetColor("_EmissionColor", onColor);
            if (index + (lampParent.transform.childCount / 2) < lampParent.transform.childCount)
            {
                current = lampParent.transform.GetChild(index + (lampParent.transform.childCount / 2)).gameObject;
            }

            current.GetComponent<Renderer>().material.SetColor("_EmissionColor", onColor);
            yield return new WaitForSeconds(waitTime);
            index++;
            if (index >= lampParent.transform.childCount/2)
            {
                index = 0;
            }
            
            
        }
        yield return new WaitForSeconds(0);
        StopAllCoroutines();
        StartCoroutine(lampies());
    }
}
