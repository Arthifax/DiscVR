using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockerPuzzleKeyScript : MonoBehaviour
{
    [SerializeField] public int currentHeldKeyNr;
    [SerializeField] public bool isHoldingKey = false;
    [SerializeField] public List<GameObject> keyList = new List<GameObject>();
    [SerializeField] public GameObject keyVisual;

    public void PickUpKey(int keyNumber)
    {
        if(!isHoldingKey)
        {
            isHoldingKey = true;
            currentHeldKeyNr = keyNumber;
            keyVisual.SetActive(true);

            for (int i = 0; i < keyList.Count; i++)
            {
                if (keyList[i].name.Equals("Keys_Hanging " + keyNumber))
                {
                    keyList[i].GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
        else if (isHoldingKey && keyNumber == currentHeldKeyNr)
        {
            isHoldingKey = false;
            currentHeldKeyNr = 0;
            keyVisual.SetActive(false);

            for (int i = 0; i < keyList.Count; i++)
            {
                if (keyList[i].name.Equals("Keys_Hanging " + keyNumber))
                {
                    keyList[i].GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}