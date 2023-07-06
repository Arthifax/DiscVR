using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblePlayerManager : MonoBehaviour
{
    [Header("Tube")]
    public bool holdingTube = false;
    public int currentTubeNumber;

    public List<GameObject> placableObjects;
    public List<GameObject> placedObjects;

    [Header("Marble")]
    public bool holdingMarble = false;
    public string currentMarbleShape;

    [Header("Cats")]
    public List<string> catsSpotted;
    private int maxCatsSpotted = 0;
    [SerializeField] private GameObject catEasterEgg;

    [SerializeField] private GatherPoints pointManager;
    [SerializeField] private bool finishedLevel = false;

    private void Start()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }

    private void Update()
    {
        if (pointManager.opdrachten.Count == 0)
        {
            if (!finishedLevel)
            {
                finishedLevel = true;
                pointManager.finishLevel();
            }
            
        }
    }

    public void SpotCat(string catName)
    {
        if (catsSpotted.Count == 0)
        {
            AddCat(catName);
        }

        foreach (string cat in catsSpotted)
        {
            if(catName == cat)
            {
                //...You clicked the cat twice
            }
            else
            {
                AddCat(catName);

                return;
            }
        }
    }

    public void AddCat(string catName)
    {
        catsSpotted.Add(catName);
        maxCatsSpotted++;
        Debug.Log(catName + " says hi!");

        if (maxCatsSpotted == 3)
        {
            catEasterEgg.gameObject.SetActive(true);
        }
    }
}
