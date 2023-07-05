using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Didgit : MonoBehaviour
{
    private int index;
    [Header("Colors")]
    public Color offColor;
    public Color onColor;
    [Space]
    [Header("Digit listis")]
    public List<InspectorSuperList> NumberList;
    public InspectorSuperList RedCapcule0;
    public InspectorSuperList GrayCapcule0;
    public InspectorSuperList RedCapcule1;
    public InspectorSuperList GrayCapcule1;
    public InspectorSuperList RedCapcule2;
    public InspectorSuperList GrayCapcule2;
    public InspectorSuperList RedCapcule3;
    public InspectorSuperList GrayCapcule3;
    public InspectorSuperList RedCapcule4;
    public InspectorSuperList GrayCapcule4;
    public InspectorSuperList RedCapcule5;
    public InspectorSuperList GrayCapcule5;
    public InspectorSuperList RedCapcule6;
    public InspectorSuperList GrayCapcule6;
    public InspectorSuperList RedCapcule7;
    public InspectorSuperList GrayCapcule7;
    public InspectorSuperList RedCapcule8;
    public InspectorSuperList GrayCapcule8;
    public InspectorSuperList RedCapcule9;
    public InspectorSuperList GrayCapcule9;

    [System.Serializable]
    public class InspectorSuperList
    {
        public List<GameObject> Capsules = new List<GameObject>();
    }
    private void Start()
    {
        NumberList.Add(RedCapcule0);
        NumberList.Add(RedCapcule1);
        NumberList.Add(RedCapcule2);
        NumberList.Add(RedCapcule3);
        NumberList.Add(RedCapcule4);
        NumberList.Add(RedCapcule5);
        NumberList.Add(RedCapcule6);
        NumberList.Add(RedCapcule7);
        NumberList.Add(RedCapcule8);
        NumberList.Add(RedCapcule9);
        NumberList.Add(GrayCapcule0);
        NumberList.Add(GrayCapcule1);
        NumberList.Add(GrayCapcule2);
        NumberList.Add(GrayCapcule3);
        NumberList.Add(GrayCapcule4);
        NumberList.Add(GrayCapcule5);
        NumberList.Add(GrayCapcule6);
        NumberList.Add(GrayCapcule7);
        NumberList.Add(GrayCapcule8);
        NumberList.Add(GrayCapcule9);
    }
    public void setNumber(int number)
    {
        if (number > -1 && number < 10)
        {
            InspectorSuperList OncurrentList = NumberList[number];
            InspectorSuperList offcurrentList = NumberList[number + 10];
            setColor(OncurrentList.Capsules, onColor);
            setColor(offcurrentList.Capsules, offColor);
        }
    }
    void setColor(List<GameObject> aList, Color color)
    {
        foreach (GameObject obj in aList)
        {
            obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        }

    }

}
