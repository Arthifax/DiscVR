using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankScenePortalActivator : MonoBehaviour
{
    public GameObject[] portals;

    private void Start()
    {
        foreach (GameObject item in portals)
        {
            item.SetActive(false);
        }
    }

    public void ActivatePortals()
    {
        foreach (GameObject item in portals)
        {
            item.SetActive(true);
        }
    }
}
