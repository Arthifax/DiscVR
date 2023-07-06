using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HostageSelection : MonoBehaviour
{
    public UnityEvent onSelectedHostage;

    public bool canSelect;


    public GameObject cam = null;
    public GameObject cameraRig = null;

    public void SetCanSelect()
    {
        canSelect = true;
    }


    public void SelectHostage()
    {
        if (canSelect)
        {
            onSelectedHostage.Invoke();
        }
    }


    private void OnDestroy()
    {
        onSelectedHostage.RemoveAllListeners();
        onSelectedHostage = null;
    }
}
