using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskLamp : MonoBehaviour {

    [SerializeField]
    GameObject light;
    public void HitByPlayer()
    {
        if (!light.activeSelf)
        {
            light.SetActive(true);
        }
        else
        {
            light.SetActive(false);
        }
    }
}
