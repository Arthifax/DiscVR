using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateCandidate : MonoBehaviour
{
    public bool Generate; 
    [Space]
    [Header("Number")]
    public int minimum = 0;
    public int max = 0;
    public TextMeshPro TextShirt;
    public TextMeshPro TextJacket;
    [Space]
    [Header("Hair")]
    public List<GameObject> hairSysles;
    [Space]
    [Header("Body")]
    public Vector2 HeightMaxMin = new Vector2(0.9f,1.2f);
    public Vector2 ThickMaxMin = new Vector2(0.9f,1.2f);
    public Vector2 scaleMaxMin = new Vector2(0.9f, 1.1f);
    public List<GameObject> scaledParts;
    public GameObject HeightObject;
    private WaitForSeconds generateDelay= new WaitForSeconds(2);


    // Start is called before the first frame update
    private void Start()
    {
        //StartCoroutine(count());
        Generate = true;
    }
    private void Update()
    {
        if (Generate)
        {
            Generate = false;
            GenerateNumber();
            GenerateHair();
            GenerateBody();
        }
    }
    
    void GenerateNumber()
    {
        int PersonNumber = Random.Range(minimum, max);
        TextShirt.SetText(PersonNumber.ToString());
        TextJacket.SetText(PersonNumber.ToString());
    }

    void GenerateHair()
    {
        foreach (GameObject hair in hairSysles)
        {
            hair.SetActive(false);
        }
        hairSysles[Random.Range(0, hairSysles.Count-1)].SetActive(true);
    }

    void GenerateBody()
    {
        HeightObject.transform.localScale = Vector3.one;
        float heightScale = Random.Range(HeightMaxMin.x, HeightMaxMin.y);
        float thickScale = Random.Range(ThickMaxMin.x, ThickMaxMin.y);
        HeightObject.transform.localScale = new Vector3(heightScale, thickScale, 1);
        foreach (GameObject bodypart in scaledParts)
        {
            bodypart.transform.localScale = new Vector3(1, 1, 1);
        }
        foreach (GameObject bodypart in scaledParts)
        {
            float scale = Random.Range(scaleMaxMin.x, scaleMaxMin.y);
            bodypart.transform.localScale = new Vector3(scale,scale,scale);
        }
    }

    IEnumerator count()
    {
        yield return generateDelay;
        Generate = true;
        StartCoroutine(count());
    }

}
