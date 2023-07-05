using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour
{
    private bool activated;

    private JaloezieCounter counter;

    private void Start()
    {
        counter = FindObjectOfType<JaloezieCounter>();
    }

    public void Activate()
    {
        if(!activated)
        {
            counter.SetCounter();
            activated = true;
        }
    }
}
